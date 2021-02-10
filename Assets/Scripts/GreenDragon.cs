using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDragon : MonoBehaviour
{
    [SerializeField]
    private float atkInterval;
    [SerializeField]
    private float atkDistance;
    private float intervalAtk;
    private bool isAtk;
    [SerializeField]
    private GameObject fireball;
    private GameObject player;
    private Vector3 firePos;
    private Animator greenDragonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        greenDragonAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = player.transform.position.x - transform.position.x;
        if (distance > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
            firePos = new Vector3(1.5f,0,0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0,180);
            firePos = new Vector3(-1.5f,0,0);
        }

        if(!isAtk && Mathf.Abs(distance) <= atkDistance)
        {
            greenDragonAnimator.SetTrigger("atk");
            Instantiate(fireball, transform.position + firePos, transform.rotation);
            isAtk = true;
        }

        if (isAtk)
        {
            intervalAtk += Time.deltaTime;

            if (intervalAtk >= atkInterval)
            {
                isAtk = false;
                intervalAtk = 0f;
            }
        }
        
    }

}
