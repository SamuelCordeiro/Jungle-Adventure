using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 8)
        {
            if(enemy.tag == "Treant")
            {
                gameObject.GetComponentInParent<Treant>().MovementController(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 8)
        {
            switch (enemy.tag)
            {
                case "Spider":
                    gameObject.GetComponentInParent<Spider>().Direction();
                    break;
                case "Wolf":
                    //gameObject.GetComponentInParent<Wolf>().Direction();
                    break;
                case "Scorpion":
                    break;
                case "Treant":
                    gameObject.GetComponentInParent<Treant>().MovementController(true);
                    break;
                default:
                    break;
            }
        }
    }
}
