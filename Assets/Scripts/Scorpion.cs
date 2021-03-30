using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private float speed;
    [SerializeField] private bool direction;
    [SerializeField] private float durationDirection;
    private float timeDirection;
    private Animator scorpionAnimator;
    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        scorpionAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    private void movement()
    {
        if(direction && life > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0,180);
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        timeDirection += Time.deltaTime;
        
        if (timeDirection >= durationDirection)
        {
            timeDirection = 0;
            direction = !direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            scorpionAnimator.SetTrigger("atk");
            if(!collider.gameObject.GetComponent<Player>().isVisible)
            {
                collider.gameObject.transform.Translate(-Vector2.right * 0.5f);
                GameController.current.RemoveLife(1);
                StartCoroutine(collider.gameObject.GetComponent<Player>().PlayerDamage(0.15f));
                collider.gameObject.GetComponent<Player>().isVisible = true;
            }
        }

        if (collider.gameObject.tag == "PointAtk")
        {
            if(life > 1)
            {
                scorpionAnimator.SetTrigger("hit");
                life--;
            }
            else
            {
                speed = 0;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                Destroy(gameObject, 0.4f);
                scorpionAnimator.SetTrigger("finalHit");
            }
        }
    }
}
