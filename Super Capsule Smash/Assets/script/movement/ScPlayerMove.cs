using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ScPlayerMove : MonoBehaviour
{
    private float XInput;
    private float YInput;
    private float coyoteTimeDuration;
    private bool grounded;
    private bool inCoyoteTime;
    private bool isJumping;
    private bool jumpBufferOn;
    private bool canJump;
    private float jumpBufferValue;
    private float jumpDuration;
    private Rigidbody2D rb;
    private Transform myTransform;
    private Vector2 movementForce;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float MaxXVelocity;
    [SerializeField] private float dragFactorGround;
    [SerializeField] private float dragFactorAir;
    [SerializeField] private float gravityScale;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBufferMaxTime;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private AnimationCurve jumpForce;
    [SerializeField] private Transform groundChecker;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        ApplyMovement();

        if (isJumping) 
            Jump();

        if (jumpBufferOn)
            JumpBufferOn();

        SpeedLimit();
        
            ApplyFriction();
    }

    private void Update()
    {
        GroundCheck();
    }

    private void Jump()
    {
        if (canJump)
        {
            canJump = false;
            Debug.Log("jump mtf");
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }

        if (jumpDuration > 0)
        {
            //rb.AddForce(Vector2.up * jumpForce.Evaluate(maxJumpTime - jumpDuration) * 10, ForceMode2D.Force);
            //rb.velocity = (new Vector2(rb.velocity.x, jumpForce.Evaluate(maxJumpTime - jumpDuration) *5));
            jumpDuration -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
            
    }
    private void JumpBufferOn()
    {
        jumpBufferValue -= Time.deltaTime;
        if (jumpBufferValue < 0)
            jumpBufferOn = false;

        if (grounded)
        {
            jumpBufferValue = 0;
            jumpBufferOn = false;
            isJumping = true;
            jumpDuration = maxJumpTime;
            canJump = true;
            Debug.Log("buffer called");
        }
    }

    private void SpeedLimit()
    {
        if (Mathf.Abs(rb.velocity.x) > MaxXVelocity)
        {
            rb.velocity = new Vector2(((rb.velocity.x)/ Mathf.Abs(rb.velocity.x))* MaxXVelocity  , rb.velocity.y);
        }
    }

    private void GroundCheck()
    {
        RaycastHit2D belowMe = Physics2D.Raycast(groundChecker.position,Vector2.down,0.2f, ground);

        if (belowMe.collider != null)
        {
            if (belowMe.transform.gameObject.layer == 6)
            {
                grounded = true;
                coyoteTimeDuration = coyoteTime;
                inCoyoteTime = true;
            }
            else
                grounded = false;
        }
        else
            grounded = false;

        coyoteTimeDuration -= Time.deltaTime;

        if (coyoteTimeDuration < 0) 
        {
            inCoyoteTime = false;
        }
    }

    private void ApplyFriction()
    {
        if (grounded)
        {
            if (XInput == 0)
                rb.velocity = new Vector2(rb.velocity.x / dragFactorGround, rb.velocity.y);
        }
        else
            rb.velocity = new Vector2(rb.velocity.x / dragFactorAir, rb.velocity.y);

    }

    private void ApplyMovement()
    {
        movementForce.Set(XInput * 100, - (gravityScale + (gravityScale * (-YInput * 10))));

        rb.AddForce(movementForce, ForceMode2D.Force);
    }

    #region Get Input
    public void LeftJoystick(Vector2 movementValue)
    {
        XInput = movementValue.x;
        if (movementValue.y <= 0)
            YInput = movementValue.y;
    }

    public void JumpInstruction(bool getInstruction)
    {
        if (getInstruction)
        {
            if (grounded || inCoyoteTime)
            {
                isJumping = getInstruction;
                jumpDuration = maxJumpTime;
                canJump = true;
                
            }
            else
            {
                jumpBufferOn = true;
                jumpBufferValue = jumpBufferMaxTime;
            }   
        }
        else
        {
            isJumping = getInstruction;
            jumpBufferOn = false;
            jumpBufferValue = jumpBufferMaxTime;
        }
    }
    #endregion
}
