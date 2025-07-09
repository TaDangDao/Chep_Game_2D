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
            BulletBase b = SimplePool.Spawn<BulletBase>(PoolType.Bullet_2, bulletPoint[i].position, bulletPoint[i].rotation);
            b.OnInit(5.2f);
        }
       
    }
}
