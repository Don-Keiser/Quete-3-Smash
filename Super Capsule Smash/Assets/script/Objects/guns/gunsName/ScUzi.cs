using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScUzi : ScAuto
{
    public override void Use(bool isUsing)
    {
        if (CanShoot(isUsing) && isUsing)
        {
            ShootGun();
        }
    }
    protected override void ShootGun()
    {
        var tempo = Instantiate(bulletGo, gunCanon.position, Quaternion.Euler(myRotation));
        //dammageScript.ApplyRecoil(-myForward.normalized, recoilForce, 0.1f);
        tempo.GetComponent<ScBulletBehav>().SetUpBullet(myForward.normalized, 1f);
        muzzleFlash.Play();
    }

    private void Update()
    {
        IsBeingHeld();
        OutOfMap();
    }
}
