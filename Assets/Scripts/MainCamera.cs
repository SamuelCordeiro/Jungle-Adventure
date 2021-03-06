﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float limitLeft;
    [SerializeField] private float limitRigth;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(player.transform.position.x > limitLeft && player.transform.position.x < limitRigth)
            {
                Vector3 newPos = new Vector3(player.transform.position.x + 2f, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
            }
        }
    }
}
