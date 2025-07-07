using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_2 : GunBase
{
    [SerializeField] Transform[] bulletPoint;
    [SerializeField] BulletBase bulletFrefab;
    public override void Shoot()
    {
        base.Shoot();
        for(int i = 0; i < bulletPoint.Length; i++)
        {
            BulletBase bullet = Instantiate(bulletFrefab, bulletPoint[i].position, bulletPoint[i].rotation);
            bullet.OnInit(5.2f);
        }
       
    }
}
