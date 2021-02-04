using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

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
                    gameObject.GetComponentInParent<Wolf>().Direction();
                    break;
                case "Scorpion":
                    break;
                default:
                    break;
            }
            //gameObject.GetComponentInParent<Spider>().Direction();
        }
    }
}
