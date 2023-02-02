using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovementController : MonoBehaviour
{
    public float maxSpeed = 15f;
    public float speedMul = 1f;

    public float accelTime = 0.01f;

    private Rigidbody2D rigidbody2D;

    private float runningTime = 0f;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 velocity = new Vector2(0f, rigidbody2D.velocity.y);

        if (0.1 < horizontal || horizontal < -0.1)
        {
            runningTime += Time.deltaTime;
            velocity.x = Math.Clamp((runningTime / accelTime) * maxSpeed, 0f, maxSpeed * speedMul) * horizontal;
        }
        else
        {
            runningTime = 0;
        }

        rigidbody2D.velocity = velocity;
    }
}