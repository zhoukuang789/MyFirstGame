using UnityEngine;

public class PlaneBasicWeapon : MonoBehaviour
{
    private float _fireTimer;

    public GameObject bullet;
    public Transform muzzle1;
    public Transform muzzle2;

    private PlaneConfig _planeConfig;

    private void Start()
    {
        _fireTimer = 0;
    }

    public void SetPlaneConfig(PlaneConfig cfg)
    {
        _planeConfig = cfg;
    }

    protected virtual void Update()
    {
        DoCooldown();
    }

    protected void  DoCooldown()
    {
        _fireTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        //射击的冷却时间
        _fireTimer += _planeConfig.fireInterval;

        //创建子弹
        var bullet1 = Instantiate(bullet, muzzle1.position, muzzle1.rotation, transform.parent);
        //bullet1.name = "我的子弹1";
        bullet1.SetActive(true);

        var bullet2 = Instantiate(bullet, muzzle2.position, muzzle2.rotation, transform.parent);
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
}
