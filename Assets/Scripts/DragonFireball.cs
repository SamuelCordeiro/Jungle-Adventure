using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireball : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    private CircleCollider2D circle;
    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            if(!collider.gameObject.GetComponent<Player>().isVisible)
            {
                StartCoroutine(collider.gameObject.GetComponent<Player>().PlayerDamage(0.15f));
                collider.gameObject.GetComponent<Player>().isVisible = true;
                //circle.enabled = false;
                collider.gameObject.transform.Translate(-Vector2.right * 0.5f);
                GameController.current.RemoveLife(1);
                Destroy(gameObject, 0.50f);
            }
        }
    }
}
