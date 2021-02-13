using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private float speedMovement;
    private float startPosition;
    private float lenght;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (mainCamera.transform.position.x * (1 - speedMovement));
        float distance = (mainCamera.transform.position.x * speedMovement);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        if(temp > startPosition + lenght / 2)
        {
            startPosition += lenght;
        }
        else if(temp < startPosition - lenght / 2)
        {
            startPosition -= lenght;
        }
    }
}
