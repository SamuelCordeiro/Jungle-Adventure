using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    private int life;
    [SerializeField]
    private float speed;
    private bool direction;
    private GameObject player;
    private Animator wolfAnimator;
    // Start is called before the first frame update
    void Start()
    {
        life = 3;
        player = GameObject.FindGameObjectWithTag("Player");
        wolfAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float xDistance = player.transform.position.x - transform.position.x;
        float yDistance = player.transform.position.y - transform.position.y;
        if(direction)
        {
            transform.eulerAngles = new Vector2(0,0);
            if(xDistance > 1 && xDistance < 7 && yDistance > - 0.5f && yDistance < 0.5f)
            {
                wolfAnimator.SetBool("isPlayerNearby", true);
                speed = 4.5f;
            }
            else
            {
                wolfAnimator.SetBool("isPlayerNearby", false);
                speed = 3f;
            }
        }
        else
        {
            transform.eulerAngles = new Vector2(0,180);
            if(xDistance < 1 && xDistance > -7 && yDistance > - 0.5f && yDistance < 0.5f)
            {
                wolfAnimator.SetBool("isPlayerNearby", true);
                speed = 4.5f;
            }
            else
            {
                wolfAnimator.SetBool("isPlayerNearby", false);
                speed = 3f;
            }
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    
    public void Direction()
    {
        direction = !direction;
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            wolfAnimator.SetTrigger("atk");
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
            if(life >= 0)
            {
                wolfAnimator.SetTrigger("hit");
                life--;
            }
            else
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                speed = 0;
                wolfAnimator.SetTrigger("finalHit");
                Destroy(gameObject, 0.4f);
            }
        }
    }
    
}
