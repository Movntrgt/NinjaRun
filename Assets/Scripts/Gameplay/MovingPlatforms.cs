using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool moveRight;

    [SerializeField] private float moveSpeed = 4f; // Speed that enemy will be walking at
    [SerializeField] private float moveDuration = 3f; // Duration that the enemy will be walking for
    [SerializeField] private float pauseDuration = 2f; // Duration that the enemy will be stationary before changig direction
    private float moveTimer;
    private float pauseTimer;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component of the enemy
        moveTimer = 0f;
        pauseTimer = 0f;
        moveRight = true;
    }

    // FixedUpdate is called once every 0.02 seconds
    private void FixedUpdate()
    {
        Moving();
    }

    private void Moving()
    {
        // The platform will move in a direction for a time of 'moveDuration'
        // at a speed of 'moveSpeed'.
        // It will then stop moving for a time of 'pauseDuration'
        // and it will change direction and repeat this process


        if (moveTimer < moveDuration && moveRight == true)
        {
            moveTimer += Time.fixedDeltaTime;
            //Debug.Log("PLatform is moving right");
           // Debug.Log("PLatform has been moving for " + moveTimer + " seconds");
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if (moveTimer < moveDuration && moveRight == false)
        {
            moveTimer += Time.fixedDeltaTime;
            //Debug.Log("Platform is walking left");
            //Debug.Log("Platform has moved for " + moveTimer + " seconds");
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else
        {
            ChangeDirection();
        }

    }

    private void ChangeDirection()
    {
        Vector3 scale = transform.localScale;

        if (pauseTimer < pauseDuration) // loops until timer is greater or equal to the duration.
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            pauseTimer += Time.fixedDeltaTime;
        }
        else
        {
            // Reset timers to zero 
            moveTimer = 0;
            pauseTimer = 0;

            // Change direction of motion
            moveRight = !moveRight;
            //Debug.Log("Changing Direction");
        }
    }
}
