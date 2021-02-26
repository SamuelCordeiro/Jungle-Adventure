using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant : MonoBehaviour
{
    private int life;
    [SerializeField] private float speed;
    [SerializeField] private bool canWalk;
    [SerializeField] private float immobileTime;
    [SerializeField] private string direction;
    private bool hit;
    [SerializeField] private bool isPlatformOver;
    [SerializeField] private float xDistance;
    private GameObject player;
    private Animator treantAnimator;

    // Start is called before the first frame update
    void Start()
    {
        life = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        treantAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        ChangeDirection();
        Hit();
        calculateImmobileTime();
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
        if(xDistance > 1)
        {
            transform.eulerAngles = new Vector2(0,0);
            direction = "rigth";
        }
        else if(xDistance < 1)
        {
            transform.eulerAngles = new Vector2(0,180);
            direction = "left";
        }
    }

    public void MovementController(bool value)
    {
        isPlatformOver = value;
    }
    
    void calculateImmobileTime()
    {
        immobileTime -= Time.deltaTime;
        if(immobileTime <= 0)
        {
            canWalk = true;
        } 
        if(immobileTime >= 0)
        {
            canWalk = false;
        }
    }
    void Hit()
    {
        if(hit)
        {
            Debug.Log("2");
            transform.Translate(Vector2.right * -100f * Time.deltaTime);
            hit = false;
            immobileTime = 0.8f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "PointAtk")
        {
            if(life > 0)
            {
                treantAnimator.SetTrigger("hit");
                hit = true;
                life--;
            }
        }        
    }
}
