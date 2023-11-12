using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScDammage : MonoBehaviour
{
    [SerializeField] ScPlayerMove moveScript;
    private Rigidbody2D rb;
    private float knockBackFactor;
    private int dammage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void applyKnockBack(Vector2 direction)
    {
        moveScript.LimitSpeedMovement(false);
        rb.AddForce(direction, ForceMode2D.Impulse);
        Invoke("RestoreSpeedLimit", dammage/100);
    }
    
    private void RestoreSpeedLimit()
    {
        moveScript.LimitSpeedMovement(true);
    }

    public void GetDammage(int dammageCount, float pushBackForce, Vector2 pushBackDirection)
    {
        dammage += dammageCount;
        if (pushBackDirection != Vector2.zero)
        {
            if (pushBackDirection.y < 0)
                applyKnockBack((pushBackDirection.normalized + Vector2.down) * pushBackForce * dammage/100);
            else
                applyKnockBack((pushBackDirection.normalized + Vector2.up) * pushBackForce * dammage / 100);
        }
    }
}
