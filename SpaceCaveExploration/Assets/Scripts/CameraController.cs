﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Transform target;

    public Transform backGround, middleGround;

    public float minHeight, maxHeaight;

    public float minWidth, maxWidth;

    private Vector2 lastPos;

    public bool stopFollowing;


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if (!stopFollowing)
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, minWidth, maxWidth), Mathf.Clamp(target.position.y, minHeight, maxHeaight), transform.position.z);


            Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

            backGround.position = backGround.position + new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleGround.position += new Vector3(amountToMove.x * .5f, amountToMove.y, 0f) * .5f;


            lastPos = transform.position;

        }


    }
}
