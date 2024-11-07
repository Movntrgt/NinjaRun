using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    // Variable related for player speeds
    // By using [SerializeField], I can alter the values in Unity rather than manually altering the values in this code.
    [SerializeField] private float maxSpeed = 100.0f; // Maximum speed that the player can move at
    [SerializeField] private float acceleration = 20.0f; // The time it takes for the player to accelerate to maxSpeed from 0
    private float targetSpeed;

    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float wallSlideSpeed = 50f;
    
    // Variable related for colliders
    private float wallCheckDistance = 0.5f;
    private float groundCheckDistance = 1.015f; // The distance that the raycast will check, it is set to this value because it takes into account the distance from the middle of the player to the bottom of the player
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 inputDirection; // 2 dimensional vector to store the horizontal and vertical input of the user.
    

    // Variables related to the player animations
    private Animator animator;
    // List representing different animation states i.e. when state = 0, idle animation is being played
    private enum MovementState { idle, running, jumping, falling} 
    

    // Boolean variables to check if the player is touching a wall or if it is grounded
    private bool isTouchingWall;
    private bool isGrounded;

    // Variables used in WallSliding() and WallJump()
    private bool isWallSliding;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpCount;
    [SerializeField] private Vector2 wallJumpPower = new Vector2(8f, 16f);


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Gets the Rigidbody2D component of the player
        animator = GetComponent<Animator>(); // Gets the Animator component of the player
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Jumping();
        WallJump();
    }

    private void FixedUpdate()
    {
        inputDirection.x = Input.GetAxis("Player2Horizontal");
        CheckForWall();
        CheckForGround();
        Running();
    }

    private void Running()
    {
        targetSpeed = inputDirection.x * maxSpeed;

        Animations();

        if (isGrounded) // If the player is not touching a wall and is on the ground
        {
            //Horizontal movement code
            if (targetSpeed > 0 && rb.velocity.x < targetSpeed) // If the player is pressing the right arrow key, is moving to the right and is not yet at the desired speed
            {                
                rb.velocity = new Vector2(rb.velocity.x + acceleration * Time.fixedDeltaTime, rb.velocity.y);
            }
            else if (targetSpeed < 0 && rb.velocity.x > targetSpeed) // If the player is pressing the left arrow key, is moving to the left and is not yet at the desired speed
            {
                rb.velocity = new Vector2(rb.velocity.x - acceleration * Time.fixedDeltaTime, rb.velocity.y);
            }
        }

        WallSliding(); // Code for wall sliding is called.
         
    }

    private void Animations()
    {
        MovementState state;
        Vector3 scale = transform.localScale;

        // Controls Running animation
        if (targetSpeed < 0f) // If the player is moving left
        {
            state = MovementState.running; // Running animation is played
            scale.x = -1f; // Player sprite faces towards the left
            transform.localScale = scale;
        }
        else if (targetSpeed > 0f) // If the player is moving to right
        {
            state = MovementState.running;
            scale.x = 1f; // Player sprite flips to face towards the right
            transform.localScale = scale;
        }
        else
        {
            state = MovementState.idle; // Idle animations is played
        }

        if (rb.velocity.y > 0) // If the player is moving upwards
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < 0) //  If the player is moving down
        {
            state = MovementState.falling;
        }

        animator.SetInteger("State", (int)state); // The 'State' value in the Unity animator is set to the value of the state value.
    }

    private void Jumping() // Checks if jump key is pressed and applies force to player if it has.
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true) // If the jump key is pressed and the player is grounded
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Vertical upwards force is applied to the player
        }

        if (Input.GetKeyDown(KeyCode.S) && isGrounded == false) // If the down arrow key is pressed and the player is not on the ground
        {
            rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse); // Player accelerates downwards
        }
    }

    private void CheckForWall() // Function to check if the player is touching a wall
    {
        Vector2 raycastRight = new Vector2(transform.right.x, 0); // A Horizontal vector pointing to the right is generated.
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, raycastRight, wallCheckDistance, groundLayer);

        Vector2 raycastLeft = new Vector2(-transform.right.x, 0); // A horizontal vector pointing left is generated.
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, raycastLeft, wallCheckDistance, groundLayer);

        if (hitRight.collider != null) // If the right-horizontal raycast detects a wall
        {
            isTouchingWall = true;
            //Debug.Log("Hit wall: " + hitRight.collider.gameObject.name);
            //Debug.Log("Distance to wall: " + hitRight.distance);
        }
        else if (hitLeft.collider != null) // If the left-horizontal raycast detects a wall
        {
            isTouchingWall = true;
            //Debug.Log("Hit wall: " + hitLeft.collider.gameObject.name);
            //Debug.Log("Distance to wall: " + hitLeft.distance);
        }
        else // If the right-horizontal raycast detects a wall
        {
            isTouchingWall = false;
            //Debug.Log(isTouchingWall + ". No wall detected");
        }
    }

    private void CheckForGround() // Function to check if the player is on the ground
    {
        Vector2 raycastDirection = new Vector2(0, -1); // A vertical vector pointing down is generated
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, groundCheckDistance, groundLayer);
        if (hit.collider != null) // If the downwards vertical raycast detects contact with the ground
        {
            isGrounded = true;
            //Debug.Log("Hit ground: " + hit.collider.gameObject.name);
            //Debug.Log("Distance to ground: " + hit.distance);
        }
        else // If the downwards vertical raycast does not detect contact with the ground
        {
            isGrounded = false;
            //Debug.Log(isGrounded + ". No ground detected");
        }
    }

    private void WallSliding() // Code for Wall Sliding
    {
        if (isTouchingWall && !isGrounded && inputDirection.x != 0) 
        { 
            //Debug.Log("Is wall sliding");
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        
        if (isWallSliding) // If the player is wall sliding
        {
            wallJumpDirection = transform.localScale.x; 
            // Sets the direction of the wall jump to the direction the sprite is facing.
            wallJumpCount = wallJumpTime; 
            // Time period in which the player is able to jump when leaving the wall.
            
        }
        else
        {
            wallJumpCount -= Time.deltaTime; 
            // Time to wall jump counts down. Allows the player to turn away from the wall and still be able to wall jump for a brief moment.        
        }

        if (Input.GetKeyDown(KeyCode.W) && wallJumpCount > 0) // If the jump key is pressed and the player can still jump
        {
            //Debug.Log("Is Wall Jumping");

            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            // Applies jump force to the x and y directions of the player.
            wallJumpCount = 0;
            // Player can no longer jump.
        }

    }

}