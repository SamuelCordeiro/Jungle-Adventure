using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeet : MonoBehaviour
{
    [SerializeField]
    private bool isGrounded;

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 8)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) 
    {
        if(collider.gameObject.layer == 8)
        {
            isGrounded = false;
        }
        
    }

    public bool GetIsGrouded()
    {
        return isGrounded;
    }
}
