using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private float speed;
    [SerializeField] private bool direction;
    [SerializeField] private float durationDirection;
    private float timeDirection;
    private Animator spiderAnimator;
    // Start is called before the first frame update
    void Start()
    {
        life = 2;
        spiderAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if(direction)
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

    public void Direction()
    {
        direction = !direction;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            spiderAnimator.SetTrigger("atk");
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
                spiderAnimator.SetTrigger("hit");
                life--;
            }
            else
            {
                speed = 0;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                Destroy(gameObject, 0.5f);
                spiderAnimator.SetTrigger("finalHit");
            }
        }
    }
}
