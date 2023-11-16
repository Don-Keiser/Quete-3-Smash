using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScBulletBehav : MonoBehaviour
{
    [SerializeField] int dammage;
    private Vector3 myDirection;
    private int mySpeed;
    private Transform myTrans;

    private void Start()
    {
        myTrans = transform;
    }

    private void FixedUpdate()
    {
        myTrans.position = myTrans.position + (myDirection * mySpeed);

        if (Mathf.Abs(myTrans.position.x) > 24 || Mathf.Abs(myTrans.position.y) > 17)
            Destroy(gameObject);
    }

    public void SetUpBullet(Vector2 direction, int speed)
    {
        myDirection = direction;
        mySpeed = speed;
    }
}
