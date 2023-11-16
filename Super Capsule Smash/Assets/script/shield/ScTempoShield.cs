using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScTempoShield : MonoBehaviour
{
    [SerializeField] GameObject shield;
    [SerializeField] Transform shieldHolder;
    [SerializeField] int decreaseRatio;
    [SerializeField] float radius;

    private GameObject myShieldGo;
    private Transform myShieldTrans;
    private bool isActiv;
    private Vector3 shieldDirection;
    private Vector3 shieldToPlayer;
    private float angleToPlayer;

    void Start()
    {
        isActiv  = false;
        shieldDirection = Vector3.right;
        myShieldGo = Instantiate(shield, transform.position + (shieldDirection.normalized * radius), Quaternion.identity);
        myShieldTrans = myShieldGo.transform;
        myShieldGo.SetActive(false);
    }
    void Update()
    {
        if (isActiv)
        {
            myShieldTrans.position = shieldHolder.position + (shieldDirection.normalized * radius);
            if (myShieldTrans.localScale.x > 0.5f)
                myShieldTrans.localScale -= new Vector3(((Time.deltaTime * 1.5f)/2) , 0, 0);
            TurnAroundPlayer();
        }
        else
        {
            if (myShieldTrans.localScale.x < 2f)
                myShieldTrans.localScale += new Vector3(((Time.deltaTime * 1.5f) / 2), 0, 0);
        }
    }

    public void SetShieldActiv(bool holdShield)
    {
        isActiv = holdShield;
        if (isActiv)
            myShieldGo.SetActive(true);
        else
            myShieldGo.SetActive(false);
    }
    public void SetshieldDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
            shieldDirection = direction;
    }

    private void TurnAroundPlayer()
    {
        shieldToPlayer = myShieldTrans.position - shieldHolder.position;

        if (myShieldTrans.position.y > shieldHolder.position.y)
        {
            angleToPlayer = Vector3.Angle(Vector3.right, shieldToPlayer);
        }
        else
        {
            angleToPlayer = 360 - Vector3.Angle(Vector3.right, shieldToPlayer);
        }


        myShieldTrans.rotation = Quaternion.Euler(0,0, angleToPlayer+90);
    }
}
