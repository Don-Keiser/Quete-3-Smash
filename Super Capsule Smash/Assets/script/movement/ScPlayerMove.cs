using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ScPlayerMove : MonoBehaviour
{
    private float XInput;
    private bool grounded;
    private Rigidbody2D rb;
    private Transform myTransform;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float MaxXVelocity;
    [SerializeField] private float dragFactor;
    [SerializeField] private Transform groundChecker;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(XInput, 0) * 100, ForceMode2D.Force);
        SpeedLimit();
        if (grounded)
            ApplyFriction();
    }

    private void Update()
    {
        GroundCheck();
    }

    public void MoveLeftAndRIght(float movementValue)
    {
        XInput = movementValue;
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
            }
            else 
                grounded = false;
        }
        else
            grounded = false;
    }

    private void ApplyFriction()
    {
        if (XInput == 0)
            rb.velocity = new Vector2(rb.velocity.x / dragFactor, rb.velocity.y);
    }
}
