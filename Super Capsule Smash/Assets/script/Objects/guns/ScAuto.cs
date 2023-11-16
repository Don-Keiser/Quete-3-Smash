using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScAuto : ScGun
{
    protected override bool CanShoot(bool playerInput)
    {
        return true;
    }
    protected override void ShootGun()
    {
        base.ShootGun();
    }
}
