using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    // Variables for Animation controller
    private Animator animator;
    private enum MovementState { idle, walking } // List for different animation states
    private bool moveRight;

    [SerializeField] private float walkSpeed = 4f; // Speed that enemy will be walking at
    [SerializeField] private float walkDuration = 3f; // Duration that the enemy will be walking for
    [SerializeField] private float pauseDuration = 2f; // Duration that the enemy will be stationary before changig direction
    private float walkTimer;
    private float pauseTimer;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component of the enemy
        animator = GetComponent<Animator>(); // Gets the Animator component of the enemy
        sprite = GetComponent<SpriteRenderer>();

        walkTimer = 0f;
        pauseTimer = 0f;
    }

    // FixedUpdate is called once every 0.02 seconds
    private void FixedUpdate()
    {
        Patrolling();
    }

    private void Animations()
    {
        MovementState state;

        // Controls Walking animation
        if (rb.velocity.x != 0) // If the enemy is moving left
        {
            state = MovementState.walking; // Running animation is played
        }
        else
        {
            state = MovementState.idle; // Idle animation is played
        }

        if (transform.localScale.x == 1f)
        {
            moveRight = true;
        }
        else 
        { 
            moveRight = false;
        }

        animator.SetInteger("State", (int)state); // The 'State' value in the Unity animator is set to the value of the state value.
    }

    private void Patrolling()
    {
        // The enemy will walk in a direction for a time of 'walkDuration'
        // at a speed of 'walkSpeed'.
        // It will then stop moving for a time of 'pauseDuration'
        // and it will change direction and repeat this process
        
        Animations();
        
        if (walkTimer < walkDuration && moveRight == true)
        {
            walkTimer += Time.fixedDeltaTime;
            //Debug.Log("Enemy is walking right");
            //Debug.Log("Enemy has walked for " + walkTimer + " seconds");
            rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
            // The enemy moves right if they are facing the right
        }
        else if (walkTimer < walkDuration && moveRight == false)
        {
            walkTimer += Time.fixedDeltaTime;
           // Debug.Log("Enemy is walking left");
            //Debug.Log("Enemy has walked for " + walkTimer + " seconds");
            rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
            // The enemy moves left if they are facing the left
        }
        else
        {
            ChangeDirection();
        }
        
    }

    private void ChangeDirection()
    {        
        Vector3 scale = transform.localScale;      

        if (pauseTimer < pauseDuration)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            pauseTimer += Time.fixedDeltaTime;
        }
        else
        {
            // Reset timers to zero 
            walkTimer = 0;
            pauseTimer = 0;
            

            // Sprite swaps direction
            scale.x *= -1;
            transform.localScale = scale;
            //Debug.Log("Changing Direction");
        }
    }
}
