using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScGun : ScObject
{
    [SerializeField] public GameObject bulletGo;
    [SerializeField] public Transform gunCanon;
    [SerializeField] protected int AmoCount;
    [SerializeField] protected bool recoil;
    [SerializeField] protected float fireRate;
    protected bool canShoot = true;
    protected virtual bool CanShoot(bool playerInput)
    {
        return true;
    }

    protected void ResetShoot()
    {
        canShoot = true;
    }

    protected virtual void ShootGun() 
    {

    }

}
