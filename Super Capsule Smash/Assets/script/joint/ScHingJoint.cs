using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScHingJoint : MonoBehaviour
{
    [SerializeField] Transform anchorPoint;
    [SerializeField] bool grav;
    private Transform trans;
    private float distanceToAnchor;
    private float Zrotation;
    private float meToAnchorAngle;
    private bool freeMovement;
    private Rigidbody2D rb;

    private void Start()
    {
        trans = transform;
        distanceToAnchor = Vector2.Distance(trans.position, anchorPoint.position);
        rb = GetComponent<Rigidbody2D>();
        freeMovement = true;
    }

    private void Update()
    {
        AngleToAnchor();
        trans.rotation = Quaternion.Euler(trans.rotation.x,trans.rotation.y, Zrotation);

        if (grav)
        {
            trans.position = trans.position + new Vector3(0,-0.01f,0);
        }
            

        if (Vector2.Distance(trans.position, anchorPoint.position) != distanceToAnchor)
        {
            if (freeMovement)
                trans.position = anchorPoint.position + (trans.position-anchorPoint.position).normalized * distanceToAnchor;
            
        }
    }
    private void AngleToAnchor()
    {
        Zrotation = Vector2.Angle(trans.position - anchorPoint.position, Vector2.right);
        if (anchorPoint.position.y < trans.position.y)
        {
            Zrotation += 180;
            meToAnchorAngle = Zrotation;
            Zrotation = (90 - (180- meToAnchorAngle));
        }
        else
        {
            meToAnchorAngle = Zrotation;
            Zrotation = 90 - meToAnchorAngle;
        }
    }

    public void MoveFreely(bool isFree)
    {
        freeMovement = isFree;
    }

    public void MoveOnCommand(float angle)
    {
        // set the angle between the vector right and the limb
        trans.position = anchorPoint.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), - Mathf.Sin(angle * Mathf.Deg2Rad), 0).normalized * distanceToAnchor;
    }
}
