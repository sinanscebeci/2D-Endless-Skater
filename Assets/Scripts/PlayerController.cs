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
    private bool isSliding;
    private bool slidePressed;
    public BoxCollider2D regularColl;
    public GameObject slideColl;

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
    private bool isTouchingCeiling;
    public Transform ceilingCheck;

    private Animator animator;
    private string currentState;
    const string idle = "Idle";
    const string run = "Run";
    const string jump = "Jump";
    const string fall = "Fall";
    const string slide = "Slide";

    //UI
    public GameObject gameOverMenu;
    public GameObject inGameHUD;

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

        //Check if player's head is touching something
        isTouchingCeiling = Physics2D.OverlapCircle(ceilingCheck.position, checkRadius, groundLayer);

        //Get input for jump
        if (Input.GetButtonDown("Jump") && (isGrounded || isSkating))
        {
            jumpPressed = true;
        }

        if(Input.GetKeyDown(KeyCode.C) && (isGrounded || isSkating) && !isSliding)
        {
            slidePressed = true;
        }

        //Animations
        if(!isSliding)
        {
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

        if(slidePressed)
        {
            StartCoroutine(Slide());
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
        if(isSkating)
            transform.position += new Vector3(speed, Mathf.Lerp(-0.3f, 0.3f, Mathf.PingPong(Time.time, 1)), 0) * Time.fixedDeltaTime;
        else
            transform.position += new Vector3(speed, 0, 0) * Time.fixedDeltaTime;    

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

    IEnumerator Slide()
    {
        ChangeAnimationState(slide);
        isSliding = true;
        slidePressed = false;
        slideColl.SetActive(true);
        regularColl.enabled = false;
        yield return new WaitForSeconds(0.5f);
        if(!isTouchingCeiling)
        {
            regularColl.enabled = true;
            slideColl.SetActive(false);
            isSliding = false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            regularColl.enabled = true;
            slideColl.SetActive(false);
            isSliding = false;
        }
        
    }

    private void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;
        animator.Play(newState);
        currentState = newState;    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == 8)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 0;
            gameOverMenu.SetActive(true);
            inGameHUD.SetActive(false);
        }
    }
    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(ceilingCheck.position, checkRadius);
    }
}
