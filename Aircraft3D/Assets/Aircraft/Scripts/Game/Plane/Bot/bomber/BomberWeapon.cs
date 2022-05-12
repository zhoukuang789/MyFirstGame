using System.Collections.Generic;
using UnityEngine;
using com;

public class BomberWeapon : PlaneWeapon
{
    public int maxBombCount = 3;
    private int _crtBombCount;

    protected override void PostStart()
    {
        base.PostStart();
        _crtBombCount = maxBombCount;
    }

    public bool HasBombsLeft()
    {
        return _crtBombCount > 0;
    }

    public override void Fire()
    {
        //射击的冷却时间
        _fireTimer = plane.planeConfig.fireInterval;
        _crtBombCount--;
        //创建子弹
        foreach (Transform muzzle in muzzles)
        {
            Debug.Log("release bomb");
            var bullet = Instantiate(this.bullet, muzzle.position, muzzle.rotation, GetSpawnBulletParent());
            bullet.SetActive(true);
            bullet.GetComponent<BulletBehaviour>().damgeValue = plane.planeConfig.fight.damage;
            bullet.GetComponent<BulletMovement>().parentSpeed = plane.movement.rb.velocity;
        }
    }

    public override void TryFire()
    {
        if (_fireTimer <= 0 && _crtBombCount > 0)
        {
            Fire();
        }
    }
}
