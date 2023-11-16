using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScColt : ScSingle
{
    public override void Use(bool isUsing)
    {
        if (CanShoot(isUsing))
            ShootGun();
    }

    protected override void ShootGun()
    {
        var tempo = Instantiate(bulletGo, gunCanon.position, Quaternion.Euler(myRotation));

        tempo.GetComponent<ScBulletBehav>().SetUpBullet(new Vector2(Mathf.Cos(myRotation.z * Mathf.Deg2Rad), Mathf.Sin(myRotation.z * Mathf.Deg2Rad)).normalized , 3);
    }

    private void Update()
    {
        IsBeingHeld();
    }
}
