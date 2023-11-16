using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScPunch : MonoBehaviour
{
    [SerializeField] ScPlayerSounds pSounds;

    [SerializeField] LayerMask playerMask;
    private ScAttack attackScript;
    private ScDammage dammageScript;
    private GameObject myBody;
    private bool isPunching;
    private bool isRegularPunch;
    private Vector2 punchDir;

    //this dictionary prevent us from using a get component every time we hit something
    private Dictionary<GameObject, ScDammage> enemiesAround = new Dictionary<GameObject, ScDammage>() ;

    private void Start()
    {
        myBody = transform.root.gameObject;
        attackScript = myBody.GetComponent<ScAttack>();
        dammageScript = myBody.GetComponent<ScDammage>();
    }


    public void EnablePunch(Vector2 punchDirection, bool isARegularPunch)
    {
        isPunching = true;
        punchDir = punchDirection;
        isRegularPunch = isARegularPunch;
        if (isPunching)
            gameObject.layer = LayerMask.NameToLayer("punch");
        else 
            gameObject.layer = LayerMask.NameToLayer("bodyPart");
    }

    public void StopPunching()
    {
        isPunching = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPunching)
        {
            WhatTheFuckAmIPunching(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPunching)
        {
            WhatTheFuckAmIPunching(collision);
        }
    }

    private void WhatTheFuckAmIPunching(Collider2D collision)
    {
        if (collision != null)
        {
            var punchedObject = collision.gameObject;
            if (punchedObject != myBody)
            {
                if (punchedObject.layer == 9)
                {
                    if (!enemiesAround.ContainsKey(punchedObject))
                    {
                        enemiesAround.Add(punchedObject, punchedObject.GetComponent<ScDammage>());
                    }// add the player to the known enemies 

                    if (isRegularPunch)
                        enemiesAround[punchedObject].GetDammage(10, 1, punchDir);
                    else
                        enemiesAround[punchedObject].GetDammage(25, 2, punchDir);

                    isPunching = false;
                    gameObject.layer = LayerMask.NameToLayer("bodyPart");
                    attackScript.LandPunch(true);
                    pSounds.RandomPunchSound();
                }

                if (punchedObject.layer == 16)
                {
                    Debug.Log("punched a guard ");
                    isPunching = false;
                    attackScript.LandPunch(false);
                    dammageScript.GetDammage(1,1,-punchDir);
                    pSounds.ShieldHitSound();
                }
            }
        }
    }
}
