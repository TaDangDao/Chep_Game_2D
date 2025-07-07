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
            BulletBase bullet = Instantiate(bulletFrefab, bulletPoint.position, bulletPoint.rotation);
            bullet.OnInit(5.2f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
