using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_1 : GunBase
{
    [SerializeField] Transform bulletPoint;
    [SerializeField] BulletBase bulletFrefab;
    public override void Shoot()
    {
        base.Shoot();
        BulletBase bullet = Instantiate(bulletFrefab,bulletPoint.position,bulletPoint.rotation);
        bullet.OnInit(5.2f);
    }
}
