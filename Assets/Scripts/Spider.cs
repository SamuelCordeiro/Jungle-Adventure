using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField]
    private int life;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool direction;
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
        SpiderDie();
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
    }

    public void Direction()
    {
        direction = !direction;
    }
    
    private void SpiderDie()
    {
        if(life <= 0)
        {
            speed = 0;
            spiderAnimator.SetTrigger("finalHit");
            Destroy(gameObject, 0.4f);
        }
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
            if(life > 0)
            {
                spiderAnimator.SetTrigger("hit");
                life--;
            }
        }
    }
}
