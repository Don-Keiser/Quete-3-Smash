using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ScObject : MonoBehaviour
{
    [SerializeField] ParticleSystem onThrowColPart;
    private Transform holder;
    protected Transform myTrans;
    protected ScDammage dammageScript;
    protected Rigidbody2D rb;
    private Vector3 rotationOffset;
    protected Vector3 myRotation;
    protected Vector2 myForward;
    protected objectState myState;

    private UIManager UImanager;


    private void Start()
    {
        UIManager.Instance.roundOver.AddListener(DeleteOnNewRound);
        rb = GetComponent<Rigidbody2D>();
        rotationOffset = new Vector3(0,0,-90);
        myTrans = transform;
        myState = objectState.idle;
    }

    #region Grab Function
    public void Grab(Transform holderTrans, ScDammage playerDammageScript )
    {
        if (myState == objectState.idle)
        {
            gameObject.layer = LayerMask.NameToLayer("grabbedObject");
            dammageScript = playerDammageScript;
            myState = objectState.held;
            holder = holderTrans;
            transform.position = holder.position;
            transform.rotation = holder.rotation;
            rb.gravityScale = 0;
        }
    }

    public bool CanGrab()
    {
        if (myState == objectState.idle)
            return true;
        else 
            return false;
    }
    #endregion

    public virtual void Use(bool isUsing)
    {
        Debug.Log("you didn't implemented anything dumbass");
    }

    protected void IsBeingHeld()
    {
        if (myState == objectState.held)
        {
            transform.position = holder.position;
            myRotation = rotationOffset + holder.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(myRotation);
            myForward.Set(Mathf.Cos(myRotation.z * Mathf.Deg2Rad), Mathf.Sin(myRotation.z * Mathf.Deg2Rad));
        }
    }

    public void ThrowObject()
    {
        rb.gravityScale = 1;
        myState = objectState.thrown;
        myTrans.position = myTrans.position + ((Vector3)myForward*2);
        gameObject.layer = LayerMask.NameToLayer("thrownObject");
        rb.AddForce(myForward * 25, ForceMode2D.Impulse);
        rb.AddTorque(150);
        holder = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision!=null)
        {
            if (myState == objectState.thrown)
            {
                var tempo = collision.gameObject;
                if (tempo.layer == 9)
                {
                    tempo.GetComponent<ScDammage>().GetDammage(2,0.5f,rb.velocity.normalized);
                    onThrowColPart.Play();
                    rb.velocity *= -1;
                }

                if (tempo.layer == 6)
                {
                    myState = objectState.idle;
                }
            }
        }
    }

    private void DeleteOnNewRound()
    {
        Destroy(gameObject);
    }
}

public enum objectState
{
    held,
    thrown,
    idle
}
