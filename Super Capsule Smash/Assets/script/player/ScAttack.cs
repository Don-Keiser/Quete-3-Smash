using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScAttack : MonoBehaviour
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform partSystem;
    [SerializeField] ScPunch leftHandPunch;
    [SerializeField] ParticleSystem hitParts;
    [SerializeField] ParticleSystem fatCharging;
    [SerializeField] float maxAttackSpeed;
    [SerializeField] float regularPunchDuration;
    [SerializeField] float fatPunchDuration;
    [SerializeField] float fatPunchLoadTime;
    [SerializeField] AudioSource oTakeAS;
    [SerializeField] AudioClip oTakeClip;

    private ScPlayerMove movementScript;
    private ScDammage dammageScript;
    private Vector2 attackDir;
    private Rigidbody2D rb;
    private float attackTimer;
    private float attackLoading;
    private bool isHoldingSomething;
    private attackState state;
    private ScObject heldObject;

    private UIManager UImanager;

    private void Start()
    {
        StopChargingPunch();
        rb = GetComponent<Rigidbody2D>();
        movementScript = GetComponent<ScPlayerMove>();
        dammageScript = GetComponent<ScDammage>();
        UIManager.Instance.roundOver.AddListener(ThrowObject);
        UIManager.Instance.roundOver.AddListener(StopChargingPunch);
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
            if (!isHoldingSomething)
                SimplePunch();
            else
            {
                heldObject.Use(true);
            }
                
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

    private void StopChargingPunch()
    {
        fatCharging.Stop();
    }

    public void AttackInstruction(bool instruction, Vector2 attackDirection)
    {
        if (state == attackState.idle && instruction)
        {
            if (!isHoldingSomething)
            {
                //movementScript.LimitSpeedMovement(false);
                attackDir = attackDirection.normalized;
                attackLoading = 0;
                state = attackState.loading;
            }
            else
            {
                state = attackState.attacking;
            }
        }// load the attack

        if (state == attackState.loading && instruction)
        {
            fatCharging.Play();
        }

        if (!instruction)
        {
            if (isHoldingSomething)
            {
                state = attackState.idle;
                heldObject.Use(false);
            }
        }

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
                    StopChargingPunch();
                }
                else
                {
                    attackTimer = regularPunchDuration;
                    leftHandPunch.EnablePunch(attackDir, true);
                    StopChargingPunch();
                }// regular punchs
            }
        }// stop loading


    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        { 
            if (!isHoldingSomething)
            {
                if (collision.gameObject.CompareTag("Objects"))
                {
                    if (collision.gameObject.GetComponent<ScObject>().CanGrab())
                    {
                        if (collision.gameObject.GetComponent<ScGun>())
                        {
                            OnTakeObjectSound();
                        }
                        isHoldingSomething = true;
                        heldObject = collision.gameObject.GetComponent<ScObject>();
                        heldObject.Grab(leftHand, dammageScript, this);
                        state = attackState.idle;
                        StopChargingPunch();
                    }
                }
            }
        }
    }

    private void OnTakeObjectSound()
    {
        oTakeAS.clip = oTakeClip;
        oTakeAS.Play();
    }

    public void ThrowObject()
    {
        if (isHoldingSomething)
        {
            isHoldingSomething = false;
            heldObject.ThrowObject();
            heldObject = null;
        }
    }

    public void DropObject()
    {
        if (isHoldingSomething)
        {
            isHoldingSomething = false;
            heldObject.Drop();
            heldObject = null;
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
