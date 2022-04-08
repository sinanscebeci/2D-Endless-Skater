using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed;

    //Jump Variables
    public float jumpForce;
    private bool jumpPressed;
    private bool isGrounded;

    /*//Extra Jump Variables
    private int extraJumps;
    public int extraJumpsValue;

    //Fine tuning of the jump
    public float gravityMultiplier = 1.5f;
    public float lowJumpMultiplier = 2f;*/

    //Ground Check Variables
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;
    private bool isSkating;
    public LayerMask skateLayer;

    private Animator animator;
    private string currentState;
    const string idle = "Idle";
    const string run = "Run";
    const string jump = "Jump";
    const string fall = "Fall";


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        //Check if player is skating
        isSkating = Physics2D.OverlapCircle(groundCheck.position, checkRadius, skateLayer);

        //Get input for jump
        if (Input.GetButtonDown("Jump") && (isGrounded || isSkating))
        {
            jumpPressed = true;
        }

        if(isSkating)
        {
            ChangeAnimationState(idle);
        }
        if(isGrounded)
        {
            ChangeAnimationState(run);
        }
        if(!isSkating && !isGrounded)
        {
            if(rb2d.velocity.y > 0)
                ChangeAnimationState(jump);
            if(rb2d.velocity.y < 0)
                ChangeAnimationState(fall);    
        }
    }
    private void FixedUpdate()
    {

        if (jumpPressed)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            jumpPressed = false;
        }

        //Player always moves
        transform.position += new Vector3(speed * Time.fixedDeltaTime, 0, 0);

        /*
        //Jump if jump button is pressed and player has extra jumps
        if (jumpPressed & extraJumps > 0)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jumpPressed = false;
            extraJumps--;
        }

        //If the player is falling make gravity faster
        if (!isGrounded & rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
        }
        //Jump higher when holding jump button
        else if (!isGrounded & rb2d.velocity.y > 0 & !Input.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.fixedDeltaTime;
        }*/

    }

    private void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;
        animator.Play(newState);
        currentState = newState;    
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
