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
    [SerializeField] ParticleSystem chargingFat;
    [SerializeField] float maxAttackSpeed;
    [SerializeField] float regularPunchDuration;
    [SerializeField] float fatPunchDuration;
    [SerializeField] float fatPunchLoadTime;
    private Vector2 attackDir;
    private Rigidbody2D rb;
    private float attackTimer;
    private float attackLoading;
    private attackState state;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = attackState.idle;
        StopChargingFat();
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

    private void StopChargingFat()
    {
        chargingFat.Stop();
    }

    public void AttackInstruction(bool instruction, Vector2 attackDirection)
    {
        if (state == attackState.idle && instruction)
        {
            //movementScript.LimitSpeedMovement(false);
            attackDir = attackDirection.normalized;
            state = attackState.loading;
            attackLoading = 0;
        }// load the attack

        if (state == attackState.loading && instruction)
        {
            chargingFat.Play();
        }

        if (!instruction && state == attackState.loading)
        {

            //movementScript.LimitSpeedMovement(false);
            state = attackState.attacking;
            attackDir = attackDirection.normalized;

            if (attackLoading > fatPunchLoadTime) //heavy punch
            {
                attackTimer = fatPunchDuration;
                leftHandPunch.EnablePunch(attackDir, false);
                chargingFat.Stop();
            }
            else
            {
                attackTimer = regularPunchDuration;
                leftHandPunch.EnablePunch(attackDir, true);
                chargingFat.Stop();
            }// regular punch
        }// perform the attack 
    }

}

public enum attackState
{
    loading,
    attacking,
    onCoolDown,
    idle
}
