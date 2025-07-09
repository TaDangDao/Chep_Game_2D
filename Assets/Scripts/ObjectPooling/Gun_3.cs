using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_3 : GunBase
{
    [SerializeField] Transform bulletPoint;
    [SerializeField] BulletBase bulletFrefab;
    public override void Shoot()
    {
        base.Shoot();
        StartCoroutine(IShoot());
    }
    public IEnumerator IShoot()
    {
        for(int i = 0; i < 3; i++)
        {
             //BulletBase b = Instantiate(bulletFrefab, bulletPoint.position, bulletPoint.rotation);
            BulletBase b = SimplePool.Spawn<BulletBase>(PoolType.Bullet_3, bulletPoint.position, bulletPoint.rotation);
            b.OnInit(5.2f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
