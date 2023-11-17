using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScUzi : ScAuto
{
    public override void Use(bool isUsing)
    {
        if (CanShoot(isUsing) && isUsing)
        {
            UziSound();
            ShootGun();
        }
    }
    protected override void ShootGun()
    {
        var tempo = Instantiate(bulletGo, gunCanon.position, Quaternion.Euler(myRotation));
        dammageScript.ApplyRecoil(-myForward.normalized, recoilForce, 0.05f);
        tempo.GetComponent<ScBulletBehav>().SetUpBullet(myForward.normalized, 1f);
        muzzleFlash.Play();
    }

    public void UziSound()
    {
        objectAudioSource.clip = objectAudioClip;
        objectAudioSource.Play();
    }

    private void Update()
    {
        IsBeingHeld();
        OutOfMap();
    }
}
