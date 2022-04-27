using System.Collections.Generic;
using UnityEngine;
using com;

public class PlaneWeapon : PlaneComponent
{
    private float _fireTimer;

    public GameObject bullet;
    public List<Transform> muzzles;

    protected override void PostStart()
    {
        _fireTimer = 0;
    }

    protected override void OnUpdate()
    {
        DoCooldown();
    }

    protected void DoCooldown()
    {
        _fireTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        //射击的冷却时间
        _fireTimer = plane.planeConfig.fireInterval;
        //Debug.Log("Fire");
        //创建子弹
        foreach (Transform muzzle in muzzles)
        {
            var bullet = Instantiate(this.bullet, muzzle.position, muzzle.rotation, GetSpawnBulletParent());
            //bullet1.name = "我的子弹1";
            bullet.SetActive(true);
            bullet.GetComponent<BulletBehaviour>().damgeValue = plane.planeConfig.fight.damage;
            bullet.GetComponent<BulletMovement>().parentSpeed = plane.movement.rb.velocity;
        }
    }

    public void TryFire()
    {
        if (_fireTimer <= 0)
        {
            Fire();
        }
    }

    Transform GetSpawnBulletParent()
    {
        return ReferenceService.instance.bulletsParent;
    }
}
