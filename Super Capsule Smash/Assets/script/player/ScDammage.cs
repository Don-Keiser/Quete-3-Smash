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
    private UIManager UImanager;

    private Transform myTrans;
    private Rigidbody2D rb;
    private Collider2D myColl;
    private ScmapManagor mapInfo;
    private int dammage;
    private bool isStunned;
    private bool isDead;
    private float stunnLenght;
    private Vector2 knockBackDir;
    private Vector2 posOnKnockBack;

    private void Start()
    {
        mapInfo = FindAnyObjectByType<ScmapManagor>();
        myColl = GetComponent<Collider2D>();
        UIManager.Instance.newRound.AddListener(PrepareForAnotherRound);
        UIManager.Instance.roundOver.AddListener(RoundOver);
        rb = GetComponent<Rigidbody2D>();
        myTrans = transform;
        UImanager = FindAnyObjectByType<UIManager>();
        UImanager.AddPlayer(this, transform);
    }

    private void Update()
    {
        
        if (!isDead)
            CheckMapBound();
        else
            myTrans.position.Set(500, 500,0);

    }

    private void FixedUpdate()
    {
        if (isStunned)
        {
            stunnLenght -= Time.deltaTime;
            if (stunnLenght < 0)
            {
                isStunned = false;
                input.CanGetInput(true);
                moveScript.LimitSpeedMovement(true);
                stunnPart.Stop();
            }

            applyKnockBack();
        }
    }

    private void applyKnockBack()
    {
        posOnKnockBack.Set(myTrans.position.x + knockBackDir.x, myTrans.position.y + knockBackDir.y);
        myTrans.position = posOnKnockBack;
    }
    

    public void GetDammage(int dammageCount, float pushBackForce, Vector2 pushBackDirection)
    {
        dammage += dammageCount;
        UImanager.UpdateDammageValue(this, dammage);
        if (pushBackDirection != Vector2.zero)
        {

            knockBackDir = (pushBackDirection.normalized) * (pushBackForce + (dammage / 50));

            knockBackDir /= 2;
            stunnPart.Play();
            input.CanGetInput(false);
            moveScript.LimitSpeedMovement(false);
            stunnLenght = 0.1f + (dammage / 1000);
            isStunned = true;
            
        }
    }


    public void CheckMapBound()
    {
        if (myTrans.position.y > 17 || myTrans.position.y < -17 || myTrans.position.x < -24 || myTrans.position.x > 24)
        {
            UImanager.PlayerOut(this);
            isDead = true;
            moveScript.enabled = false;
        }
    }
    private void RoundOver()
    {
        
        input.CanGetInput(false);
        moveScript.ResetInputOnNewRound();
        moveScript.enabled = false;
    }

    private void PrepareForAnotherRound(int time)
    {
        Invoke("ItIsGoodDayToBeNotDead", time);
        rb.velocity = Vector2.zero;
        dammage = 0;
        transform.position = mapInfo.GetCurrentMapSpawnPoint();
        UImanager.UpdateDammageValue(this, dammage);
        myColl.enabled = false;
    }

    private void ItIsGoodDayToBeNotDead()
    {
        isDead = false;
        moveScript.enabled = true;
        input.CanGetInput(true);
        myColl.enabled = true;
    }
}
