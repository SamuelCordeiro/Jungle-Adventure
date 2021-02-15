using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject feet;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private bool isJumping;
    public bool isAtk;
    public bool isVisible;
    public float visibleTime;
    public GameObject weapon;
    public GameObject weaponPoint;
    private float visibleCount;
    private float timeAtk;
    private Rigidbody2D playerRigidbody;
    private Animator playerAnimator;
    private SpriteRenderer playerSpriteRenderer;

    public bool leftArrowButton;
    public bool rightArrowButton;
    public bool jumpButton;
    public bool atkButton;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() 
    {
        if(feet.GetComponentInParent<PlayerFeet>().GetIsGrouded())
        {
            isJumping = false;
            playerAnimator.SetBool("isJumping", false);
        }
        atk();
        timeAtk -= Time.deltaTime;

        if(isVisible)
        {
            visibleCount += Time.deltaTime;
            if(visibleCount >= visibleTime)
            {
                visibleCount = 0;
                isVisible = false;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
        jump();
    }

    private void movement()
    {
        if(Input.GetKey(KeyCode.D) || rightArrowButton)
        {
            playerRigidbody.velocity = new Vector2(speed * Time.deltaTime, playerRigidbody.velocity.y);
            playerAnimator.SetBool("isWalking", true);
            transform.localEulerAngles = new Vector3(0,0,0);
        }
        else
        {
            playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
            playerAnimator.SetBool("isWalking", false);
        }
        if(Input.GetKey(KeyCode.A) || leftArrowButton)
        {
            playerRigidbody.velocity = new Vector2(-speed * Time.deltaTime, playerRigidbody.velocity.y);
            playerAnimator.SetBool("isWalking", true);
            transform.localEulerAngles = new Vector3(0,180,0);
        }
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) || jumpButton && !isJumping)
        {
            playerRigidbody.velocity = Vector2.up * jumpForce;
            isJumping = true;
            playerAnimator.SetBool("isJumping", true);
        }
    }

    private void atk()
    {
        if(Input.GetKeyDown(KeyCode.E) || atkButton && !isAtk)
        {
            playerAnimator.SetBool("isAtk", true);
            isAtk = true;
            timeAtk = 0.25f;
        }
        else if(timeAtk <= 0f)
        {
            playerAnimator.SetBool("isAtk", false);
            isAtk = false;
        }
    }
    // private void OnCollisionEnter2D(Collision2D collision) 
    // {
        // if(collision.gameObject.layer == 8)
        // {
        //     isJumping = false;
        //     playerAnimator.SetBool("isJumping", false);
        // } 
    // }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.gameObject.tag == "GameOver")
        {
            GameController.current.gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }
        if(collider.gameObject.tag == "Spikes")
        {
            GameController.current.RemoveLife(1);
        }
    }

    public IEnumerator PlayerDamage(float damageTime)
    {
        playerSpriteRenderer.enabled = false;
        weapon.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(damageTime);
        playerSpriteRenderer.enabled = true;
        weapon.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(damageTime);
        playerSpriteRenderer.enabled = false;
        weapon.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(damageTime);
        playerSpriteRenderer.enabled = true;
        weapon.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(damageTime);
        playerSpriteRenderer.enabled = false;
        weapon.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(damageTime);
        playerSpriteRenderer.enabled = true;
        weapon.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void MobileButtons(int key)
    {
        switch (key)
        {
            case 0:
                leftArrowButton = !leftArrowButton;
                break;
            case 1:
                rightArrowButton = !rightArrowButton;
                break;
            case 2:
                jumpButton = !jumpButton;
                break;
            case 3:
                atkButton = !atkButton;
                break;
            default:
                break;
        }
    }
    
}
