using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public ConstantForce _cf;
    private PlaneConfig _cfg;

    private float fireTimer;

    public GameObject bullet;
    public Transform muzzle1;
    public Transform muzzle2;

    private void Start()
    {
        _cf.force = transform.forward.normalized * rb.mass * ConfigService.instance.planesConfig.G;
        _cfg = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        SetSpeedOne(_cfg.move.initialSpeed);

        fireTimer = 0;
    }

    private void Update()
    {
        // 按住W加速，如果速度超过最大飞行速度不能再加速
        if (Input.GetKey(KeyCode.W) && MySpeed <= _cfg.move.maxSpeed)
        {
            SetSpeedOne(MySpeed + _cfg.move.acceleration * Time.deltaTime);
        }

        // 按住S减速，如果速度小于最小飞行速度不能再减速
        if (Input.GetKey(KeyCode.S) && MySpeed > _cfg.move.minSpeed)
        {
            SetSpeedOne(MySpeed - _cfg.move.deceleration * Time.deltaTime);
        }

        // 不按加速减速按键时，如果飞机速度大于初始速度，飞机会按自然减速度进行减速，直到减到初始速度
        if (MySpeed > _cfg.move.initialSpeed && !Input.GetKey((KeyCode.W)) && !Input.GetKey(KeyCode.S))
        {
            SetSpeedOne(MySpeed - _cfg.move.naturalDeceleration * Time.deltaTime);
        }

        // 不按加速减速按键时，如果飞机速度小于初始速度，飞机会按自然加速度进行加速，直到减到初始速度
        if (MySpeed < _cfg.move.initialSpeed && !Input.GetKey((KeyCode.W)) && !Input.GetKey(KeyCode.S))
        {
            SetSpeedOne(MySpeed + _cfg.move.naturalAcceleration * Time.deltaTime);
        }

        // 按下空格键或鼠标Y轴增大，飞机向上俯仰
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Mouse Y") > 0)
        {
            Pitch(_cfg.move.pitchRateCurve.Evaluate(MySpeed / _cfg.move.maxSpeed) * _cfg.move.pitchScaleFactor * Time.deltaTime);
        }

        // 按下Alt或鼠标Y轴减小，飞机向下俯仰
        if (Input.GetKey(KeyCode.AltGr) || Input.GetAxis("Mouse Y") < 0)
        {
            Pitch(-_cfg.move.pitchRateCurve.Evaluate(MySpeed / _cfg.move.maxSpeed) * _cfg.move.pitchScaleFactor * Time.deltaTime);
        }

        // 鼠标X轴增大，飞机向右滚转
        if (Input.GetAxis("Mouse X") > 0)
        {
            Roll(_cfg.move.rollRateCurve.Evaluate(MySpeed / _cfg.move.maxSpeed) * _cfg.move.rollScaleFactor * Time.deltaTime);
        }

        // 鼠标X轴减小，飞机向左滚转
        if (Input.GetAxis("Mouse X") < 0)
        {
            Roll(-_cfg.move.rollRateCurve.Evaluate(MySpeed / _cfg.move.maxSpeed) * _cfg.move.rollScaleFactor * Time.deltaTime);
        }

        // 按下D键，飞机右偏航
        if (Input.GetKey(KeyCode.D))
        {
            Yaw(_cfg.move.yawRateCurve.Evaluate(MySpeed / _cfg.move.maxSpeed) * _cfg.move.yawScaleFactor * Time.deltaTime);
        }

        // 按下A键，飞机左偏航
        if (Input.GetKey(KeyCode.A))
        {
            Yaw(-_cfg.move.yawRateCurve.Evaluate(MySpeed / _cfg.move.maxSpeed) * _cfg.move.yawScaleFactor * Time.deltaTime);
        }

        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }

    void Fire()
    {
        //射击的冷却时间
        fireTimer += _cfg.fireInterval;

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

    private void FixedUpdate()
    {
        // 如果飞机速度小于维持升力速度，将受到重力
        if (MySpeed < _cfg.move.stayFlySpeed)
        {
            _cf.enabled = false;
        }
        else
        {
            _cf.enabled = true;
        }
    }

    /**
     * 获取飞机实时速度
     */
    public float MySpeed
    {
        get { return rb.velocity.magnitude; }
    }

    /**
     * 设置飞机实时速度
     */
    public void SetSpeedOne(float speed)
    {
        rb.velocity = transform.forward.normalized * speed;
    }

    /**
     * 俯仰操作
     */
    private void Pitch(float angle)
    {
        AddRotate(angle, -transform.right);
    }

    /**
     * 滚转操作
     */
    private void Roll(float angle)
    {
        AddRotate(angle, -transform.forward);
    }

    /**
     * 偏航操作
     */
    private void Yaw(float angle)
    {
        AddRotate(angle, transform.up);
    }

    void AddRotate(float angle, Vector3 axis)
    {
        Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
        transform.rotation = quaternion * transform.rotation;
    }
}