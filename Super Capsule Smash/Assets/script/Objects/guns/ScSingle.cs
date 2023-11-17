using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ScSingle : ScGun
{
    protected override bool CanShoot(bool playerInput)
    {
        if (playerInput)
        {
            if (canShoot && AmoCount > 0 && (Time.time - lastFireTime) > fireDelay)
            {
                AmoCount--;
                canShoot = false;
                lastFireTime = Time.time;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            ResetShoot();
            return false;
        }
    }
    

    protected override void ShootGun()
    {
        base.ShootGun();
    }
}
