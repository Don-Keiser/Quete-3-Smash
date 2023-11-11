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
    private bool triggerBuffer;
    private bool canDoubleJump;
    private float jumpBufferValue;
    private float jumpDuration;
    private Rigidbody2D rb;
    private Transform myTransform;
    private Vector2 movementForce;
    private Vector2 exitJumpSpeed;
    private float exitJumpPreviousPos;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float MaxXVelocity;
    [SerializeField] private float dragFactorGround;
    [SerializeField] private float dragFactorAir;
    [SerializeField] private float gravityScale;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBufferMaxTime;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float legMaxSwing; // leg animation max swing in degree 
    [SerializeField] private AnimationCurve jumpForce;
    [SerializeField] private AnimationCurve legCurve;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private ScHingJoint LArm;
    [SerializeField] private ScHingJoint LLeg;
    [SerializeField] private ScHingJoint RLeg;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
        canDoubleJump = true;
        LArm.MoveFreely(false);
    }
    private void FixedUpdate()
    {
        if (isJumping) 
            Jump();

        if (jumpBufferOn)
            JumpBufferOn();

        SpeedLimit();
        ApplyFriction();
        ApplyMovement();
    }
    private void Update()
    {
        GroundCheck();
        AnimateBody();
    }

    private void Jump()
    {
        if (jumpDuration > 0)
        {
            //rb.AddForce(Vector2.up * jumpForce.Evaluate(maxJumpTime - jumpDuration) * 10, ForceMode2D.Force);
            //rb.velocity = (new Vector2(rb.velocity.x, jumpForce.Evaluate(maxJumpTime - jumpDuration)));
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + (jumpForce.Evaluate(maxJumpTime - jumpDuration)*3) ,0);
            jumpDuration -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }

        if (!grounded)
            jumpBufferOn = true;
    }
    private void CancelJump()
    {

        exitJumpSpeed.Set(rb.velocity.x, ((myTransform.position.y - exitJumpPreviousPos) / (maxJumpTime- jumpDuration)) / 2 );

        if (exitJumpSpeed.y != float.PositiveInfinity && exitJumpSpeed.y != float.NegativeInfinity)
            rb.velocity = exitJumpSpeed;
        //rb.AddForce(Vector2.up * jumpForce.Evaluate(jumpDuration), ForceMode2D.Impulse);
    }
    private void JumpBufferOn()
    {
        //player try to use the jump buffer 
        if (triggerBuffer)
        {
            jumpBufferValue -= Time.deltaTime;
            
            if (jumpBufferValue < 0)
            {
                triggerBuffer = false;
            }// buffer occured too soon 
            
            if (grounded)
            {
                SetUpJumpParameters();
                jumpBufferOn = false;
                triggerBuffer = false;
            }//activate the jump buffer
        }
        else 
        {
            if (grounded)
            {
                jumpBufferValue = 0;
                jumpBufferOn = false;
                isJumping = false;
                jumpDuration = maxJumpTime;
            }
        }
    }
    private void SetUpJumpParameters()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        isJumping = true;
        exitJumpPreviousPos = myTransform.position.y;
        jumpDuration = maxJumpTime;
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
                canDoubleJump = true;
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
    private void ApplyMovement()
    {
        if (!isJumping)
        {
            if (YInput < 0)
                movementForce.Set(XInput * 100, -(gravityScale + (gravityScale * (-YInput * 10))));
            else
                movementForce.Set(XInput * 100, -gravityScale);
        }
        else 
            movementForce.Set(XInput * 100, 0);

        rb.AddForce(movementForce, ForceMode2D.Force);
    }
    private void SpeedLimit()
    {
        if (Mathf.Abs(rb.velocity.x) > MaxXVelocity)
        {
            rb.velocity = new Vector2(((rb.velocity.x) / Mathf.Abs(rb.velocity.x)) * MaxXVelocity, rb.velocity.y);
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

    private void AnimateBody()
    {
        if (XInput != 0 && YInput != 0)
        {
            LArm.MoveFreely(false);
            if (YInput>0)
                LArm.MoveOnCommand( 180 + (180- Vector2.Angle(new Vector2(XInput, YInput).normalized, Vector2.right)));
            else
                LArm.MoveOnCommand(Vector2.Angle(new Vector2(XInput, YInput).normalized, Vector2.right));
        }
        else
            LArm.MoveFreely(true);



        if (XInput != 0 && grounded) 
        {
            LLeg.MoveFreely(false);
            RLeg.MoveFreely(false);

            LLeg.MoveOnCommand( 90 + (  (legCurve.Evaluate(Time.realtimeSinceStartup) * legMaxSwing) * Mathf.Abs(XInput) ) );
            RLeg.MoveOnCommand(90 - ((legCurve.Evaluate(Time.realtimeSinceStartup) * legMaxSwing) * Mathf.Abs(XInput)));
        }//animate legs on ground
        else
        {
            LLeg.MoveFreely(true);
            RLeg.MoveFreely(true);
        }
    }


    #region Get Input
    public void LeftJoystick(Vector2 movementValue)
    {
        XInput = movementValue.x;

        YInput = movementValue.y;
    }

    public void JumpInstruction(bool getInstruction)
    {
        if (getInstruction)
        {
            if (((grounded || inCoyoteTime))  && !isJumping) // this triggers the first jump 
            {
                SetUpJumpParameters();
            }

            if (!grounded && canDoubleJump)
            {
                SetUpJumpParameters();
                canDoubleJump = false;
            }

            if (jumpBufferOn)
            {
                triggerBuffer = true;
                jumpBufferValue = jumpBufferMaxTime;
            }
        }
        else
        {
            triggerBuffer = false;
            if (isJumping)
            {
                CancelJump();
            }
            isJumping = false;
        }
    }
    #endregion
}
