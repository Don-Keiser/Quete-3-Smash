using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ScObject : MonoBehaviour
{
    private Transform holder;
    private ScAttack playerAttackScript;
    private bool isBeingHeld;
    private Vector3 rotationOffset;
    protected Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationOffset = new Vector3(0,0,-90);
    }
    private void Update()
    {
        if (isBeingHeld) 
        {
            transform.position = holder.position;
            transform.rotation = Quaternion.Euler(rotationOffset + holder.rotation.eulerAngles);
        }
    }

    #region Grab Function
    public void Grab(Transform holderTrans, ScAttack attackScript )
    {
        SetHeld(true);
        holder = holderTrans;
        playerAttackScript = attackScript;
        rb.gravityScale = 0;
        gameObject.layer = LayerMask.NameToLayer("grabbedObject");
        transform.position = holder.position;
        transform.rotation = holder.rotation;
    }
    public void SetHeld(bool hasBeenAdded)
    {
        isBeingHeld = hasBeenAdded;
    }
    public bool IsHeld()
    {
        return isBeingHeld;
    }
    #endregion

    public virtual void Use()
    {
        Debug.Log("sexe");
    }
}
