using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAuto : ScGun
{
    protected override bool CanShoot(bool playerInput)
    {
            if (AmoCount > 0 && (Time.time - lastFireTime) > fireDelay)
            {
                AmoCount--;
                lastFireTime = Time.time;
                return true;
            }
            else
            {
                return false;
            }
    }
    protected override void ShootGun()
    {
        base.ShootGun();
    }
}
