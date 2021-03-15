using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private float speed;
    [SerializeField] private bool canWalk;
    [SerializeField] private float immobileTime;
    [SerializeField] private bool isPlatformOver;
    [SerializeField] private float xDistance;
    private bool hit;
    private GameObject player;
    private Animator treantAnimator;

    // Start is called before the first frame update
    void Start()
    {
        canWalk = true;
        life = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        treantAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ChangeDirection();
    }

    void Movement()
    {
        xDistance = player.transform.position.x - transform.position.x;

        if(xDistance >= -10f && xDistance <= 0f && !isPlatformOver && canWalk)
        {
            treantAnimator.SetBool("isPlayerNear", true);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if(xDistance <= 10f && xDistance >= 0f && !isPlatformOver && canWalk)
        {
            treantAnimator.SetBool("isPlayerNear", true);
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
           treantAnimator.SetBool("isPlayerNear", false); 
        }
    }

    void ChangeDirection()
    {
        if(xDistance > 2)
        {
            transform.eulerAngles = new Vector2(0,0);
        }
        else if(xDistance < -2)
        {
            transform.eulerAngles = new Vector2(0,180);
        }
    }

    public void MovementController(bool value)
    {
        isPlatformOver = value;
    }

    private IEnumerator Hit()
    {
        transform.Translate(Vector2.right * -100f * Time.deltaTime);
        treantAnimator.SetBool("canWalk", false);
        canWalk = false;
        yield return new WaitForSeconds(0.8f);
        treantAnimator.SetBool("canWalk", true);
        canWalk = true;
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            treantAnimator.SetTrigger("atk");
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
            treantAnimator.SetTrigger("hit");
            StartCoroutine(Hit());
            life--;
            if(life <= 0)
            {
                treantAnimator.SetTrigger("finalHit");
                gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                speed = 0;
                Destroy(gameObject, 1.2f);
            }
            // if(life >= 0)
            // {
            //     treantAnimator.SetTrigger("hit");
            //     StartCoroutine(Hit());
            //     life--;
            // }
            // else
            // {
            //     treantAnimator.SetTrigger("finalHit");
            //     gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            //     speed = 0;
            //     Destroy(gameObject, 0.4f);
            // }
        }        
    }
}
