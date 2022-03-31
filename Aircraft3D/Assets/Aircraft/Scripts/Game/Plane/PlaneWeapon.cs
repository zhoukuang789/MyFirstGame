using UnityEngine;
using System.Collections;

public class PlaneWeapon : MonoBehaviour
{
    private float fireTimer;

    public GameObject bullet;
    public Transform muzzle1;
    public Transform muzzle2;

    private PlaneConfig _planeConfig;

    private void Start()
    {
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;

        fireTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }

    void Fire()
    {
        //射击的冷却时间
        fireTimer += _planeConfig.fireInterval;

        //创建2 子弹
        var bullet1 = Instantiate(bullet, muzzle1.position, muzzle1.rotation, transform.parent);
        bullet1.name = "我的子弹1";
        bullet1.SetActive(true);

        var bullet2 = Instantiate(bullet, muzzle2.position, muzzle2.rotation, transform.parent);
        bullet2.name = "我的子弹2";
        bullet2.SetActive(true);
    }

    void TryFire()
    {
        if (fireTimer <= 0)
        {
            Fire();
            return;
        }

        fireTimer -= Time.deltaTime;
    }
}
