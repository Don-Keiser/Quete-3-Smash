using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform partSystem;
    [SerializeField] ScPlayerMove movementScript;
    [SerializeField] ParticleSystem hitParts;
    [SerializeField] float maxAttackSpeed;
    [SerializeField] float attackDuration;
    private Vector2 attackDir;
    private Rigidbody2D rb;
    private bool isAttacking;
    private bool onCoolDown;
    private float attackTimer;

    private void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            //rb.velocity = attackDir.normalized * maxAttackSpeed;
            transform.position += (Vector3)attackDir/25;

            if (attackTimer < 0) 
            {
                partSystem.position = leftHand.position;
                hitParts.Play();
                Invoke("StopPart", hitParts.main.duration);
                isAttacking = false;
                rb.velocity = Vector3.zero;
                movementScript.LimitSpeedMovement(true);
            }
        }
    }

    private void StopPart()
    {
        hitParts.Stop();
        onCoolDown = false;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void AttackInstruction(Vector2 attackDirection)
    {
        if (!isAttacking && !onCoolDown)
        {
            movementScript.LimitSpeedMovement(false);
            attackDir = attackDirection;
            isAttacking = true;
            onCoolDown = true;
            attackTimer = attackDuration;
        }
    }

    private void SpeedLimitOnAttack()
    {
        if (rb.velocity.magnitude > maxAttackSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxAttackSpeed;
        }
    }
}
