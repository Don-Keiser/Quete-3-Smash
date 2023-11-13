using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public class ScDammage : MonoBehaviour
{
    [SerializeField] ScPlayerMove moveScript;
    [SerializeField] ScGetInput input;
    [SerializeField] ParticleSystem stunnPart;
    private Transform myTrans;
    private Rigidbody2D rb;
    private float knockBackFactor;
    private int dammage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myTrans = transform;
    }

    private void applyKnockBack(Vector2 direction)
    {
        input.CanGetInput(false);
        moveScript.LimitSpeedMovement(false);
        rb.velocity = Vector2.zero;
        rb.AddForce(direction, ForceMode2D.Impulse);
        Invoke("RestoreControl", dammage/50);
        stunnPart.Play();
    }
    
    private void RestoreControl()
    {
        input.CanGetInput(true);
        moveScript.LimitSpeedMovement(true);
        stunnPart.Stop();
    }

    public void GetDammage(int dammageCount, float pushBackForce, Vector2 pushBackDirection)
    {
        dammage += dammageCount;
        if (pushBackDirection != Vector2.zero)
        {
            if (pushBackDirection.y < 0)
                applyKnockBack((pushBackDirection.normalized + Vector2.down * 2) * (pushBackForce + (dammage / 50)));
            else
                applyKnockBack((pushBackDirection.normalized + Vector2.up * 2) * (pushBackForce + (dammage / 50)));
        }
    }
}
