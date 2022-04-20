using UnityEngine;
using com;

public class PlaneWeapon : PlaneComponent
{
    private float _fireTimer;

    public GameObject bullet;
    public Transform muzzle1;
    public Transform muzzle2;

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
        _fireTimer += plane.planeConfig.fireInterval;
        Debug.Log("Fire");
        //创建子弹
        var bullet1 = Instantiate(bullet, muzzle1.position, muzzle1.rotation, GetSpawnBulletParent());
        //bullet1.name = "我的子弹1";
        bullet1.SetActive(true);

        var bullet2 = Instantiate(bullet, muzzle2.position, muzzle2.rotation, GetSpawnBulletParent());
        //bullet2.name = "我的子弹2";
        bullet2.SetActive(true);
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
