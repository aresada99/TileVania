using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb2d;

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 11.2f;
    [SerializeField] float climbingSpeed = 5f;
    float gravityScaleAtStart;

    SpriteRenderer playerSprite;
    Animator playerAnimator;
    CapsuleCollider2D playerCollider;
    BoxCollider2D feetCollider;

    float coyoteTime = 0.1f;
    float coyoteTimeCounter;

    bool ladderFirstTime = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rb2d.gravityScale;
    }

    void Update()
    {
        Run();
        Jump();
        Climbing();


    }


   void Climbing()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbable")))
        {
    
            if (ladderFirstTime)
            {
                rb2d.velocity = new Vector2(0f, 0f);
                ladderFirstTime = false;
            }
            
            rb2d.gravityScale = 0f;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, climbingSpeed);
                playerAnimator.SetBool("isClimbing", true);
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                playerAnimator.SetBool("isClimbing", false);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -climbingSpeed);
                playerAnimator.SetBool("isClimbing", true);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                playerAnimator.SetBool("isClimbing", false);
            }
            
        }
        else
        {
            rb2d.gravityScale = gravityScaleAtStart;
            playerAnimator.SetBool("isClimbing", false);
            ladderFirstTime = true;
        }
        

    }



    void Run()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.velocity = new Vector2(movementSpeed, rb2d.velocity.y);
            if (rb2d.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else if (rb2d.velocity.x > 0)
            {

                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            if (rb2d.velocity.x < 0)
            {

                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else if (rb2d.velocity.x > 0)
            {

                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.velocity = new Vector2(-movementSpeed, rb2d.velocity.y);
            if (rb2d.velocity.x < 0)
            {

                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else if (rb2d.velocity.x > 0)
            {

                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            if (rb2d.velocity.x < 0)
            {

                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else if (rb2d.velocity.x > 0)
            {

                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                playerAnimator.SetBool("isRunning", isGrounded());
            }
            else
            {
                playerAnimator.SetBool("isRunning", false);
            }
        }
    }

    void Jump()
    {
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter = coyoteTimeCounter - Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.UpArrow) && coyoteTimeCounter > 0f)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            coyoteTimeCounter = 0f;
        }
    }


    bool isGrounded()
    {
        return feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }


 

} 
