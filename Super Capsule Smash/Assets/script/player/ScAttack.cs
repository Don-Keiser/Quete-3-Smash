using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform partSystem;
    [SerializeField] ScPlayerMove movementScript;
    [SerializeField] ScPunch leftHandPunch;
    [SerializeField] ParticleSystem hitParts;
    [SerializeField] float maxAttackSpeed;
    [SerializeField] float regularPunchDuration;
    [SerializeField] float fatPunchDuration;
    [SerializeField] float fatPunchLoadTime;
    private Vector2 attackDir;
    private Rigidbody2D rb;
    private float attackTimer;
    private float attackLoading;
    private bool isHoldingSomething;
    private attackState state;
    private ScObject heldObject;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = attackState.idle;
    }

    private void FixedUpdate()
    {
        if (state == attackState.loading)
        {
            // load the attack
            attackLoading += Time.deltaTime;
        }

        if (state == attackState.attacking)
        {
            SimplePunch();
        }
    }

    #region punch
    private void SimplePunch()
    {
        // move the player in the punch direction 
        attackTimer -= Time.deltaTime;
        transform.position += (Vector3)attackDir / 2;

        if (attackTimer < 0)
        {
            LandPunch(false);
        } // player didn't hit anything
    }
    public void LandPunch(bool didLandThePunch)
    {
        if (didLandThePunch)
        {
            hitParts.Play();
        }
        Invoke("StopPart", 0.9f);
        state = attackState.onCoolDown;
        rb.velocity = Vector3.zero;
        movementScript.LimitSpeedMovement(true);
        leftHandPunch.StopPunching();
        attackLoading = 0;
    }
    private void StopPart()
    {
        hitParts.Stop();
        state = attackState.idle;
    }
    #endregion

    public void AttackInstruction(bool instruction ,Vector2 attackDirection)
    {
        if (state == attackState.idle && instruction)
        {
            if (!isHoldingSomething)
            {
                //movementScript.LimitSpeedMovement(false);
                attackDir = attackDirection.normalized;
                state = attackState.loading;
                attackLoading = 0;
            }
            else
                heldObject.Use();

        }// load the attack

        if (!instruction && state == attackState.loading)
        {
            if (!isHoldingSomething)
            {
                //movementScript.LimitSpeedMovement(false);
                state = attackState.attacking;
                attackDir = attackDirection.normalized;

                if (attackLoading > fatPunchLoadTime) //heavy punch
                {
                    attackTimer = fatPunchDuration;
                    leftHandPunch.EnablePunch(attackDir, false);
                }
                else
                {
                    attackTimer = regularPunchDuration;
                    leftHandPunch.EnablePunch(attackDir, true);
                }// regular punchs
            }
        }// perform the attack 
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        { 
            if (!isHoldingSomething)
            {
                if (collision.gameObject.CompareTag("Objects"))
                {
                    isHoldingSomething = true;
                    heldObject = collision.gameObject.GetComponent<ScObject>();
                    heldObject.Grab(leftHand,this);
                }
            }
        }
    }
}

public enum attackState
{
    loading, 
    attacking,
    onCoolDown,
    idle
}
