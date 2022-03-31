using System;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public Rigidbody rb;

    private PlaneConfig _planeConfig;

    private Vector3 _combinedForce;

    public PlaneEngine engine;

    // Start is called before the first frame update
    void Start()
    {
        // 获取飞机配置
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        // 设置刚体质量
        rb.mass = _planeConfig.movement.mass;
        // 设置刚体初始速度
        rb.velocity = transform.forward * _planeConfig.movement.initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private float _airDensity;
    private Vector3 _lift;
    private Vector3 _drag;
    private Vector3 _thrust;

    private void FixedUpdate()
    {
        // 1.空气密度
        //float airDensity = AirMechanismAlgorithm.GetAirDensity(transform.position.y);
        _airDensity = AirMechanismAlgorithm.GetAirDensity(transform.position.y);
        // 2.飞机速度大小
        float currentSpeed = rb.velocity.magnitude;
        // 3.飞机迎角
        float attackOfAngle = 90;
        var attackOfAngleProject = Vector3.ProjectOnPlane(rb.velocity, transform.right);
        if (attackOfAngleProject != Vector3.zero)
        {
            attackOfAngle = Vector3.Angle(transform.forward, attackOfAngleProject);
        }

        var relativeForwardSpeed = Vector3.Project(rb.velocity, transform.forward);
        // 4.升力系数
        float liftCoefficient = _planeConfig.movement.liftCoefficientCurve.Evaluate(Mathf.Clamp(attackOfAngle, -10, 25));
        // ==>5.升力大小
        float liftMagnitude = 0.5f * _airDensity * relativeForwardSpeed.magnitude * relativeForwardSpeed.magnitude * _planeConfig.movement.wingArea * liftCoefficient;
        // 6.升力方向
        Vector3 liftDirection = Vector3.Cross(-transform.right, rb.velocity).normalized;
        // 7.升力
        //Vector3 lift = liftDirection * liftMagnitude;
        //TODO 目前的唯一的问题是升力偏大了，大约50倍左右，其他问题基本解决了
        _lift = liftDirection * liftMagnitude*0.02f;

        Debug.Log("relativeForwardSpeed " + relativeForwardSpeed.magnitude + "\nattackOfAngle " + attackOfAngle + "\nliftCoefficient " + liftCoefficient + "\nliftMagnitude " + liftMagnitude);
        // 1.阻力系数
        float dragCoefficient = _planeConfig.movement.dragCoefficientCurve.Evaluate(Mathf.Clamp(attackOfAngle, -10, 19));
        // 2.阻力大小
        float dragMagnitude = 0.5f * _airDensity * relativeForwardSpeed.magnitude * relativeForwardSpeed.magnitude * _planeConfig.movement.wingArea * dragCoefficient;
        // 3.阻力方向
        Vector3 dragDirection = -rb.velocity.normalized;
        // 4.阻力
        //Vector3 drag = dragDirection * dragMagnitude;
        _drag = dragDirection * dragMagnitude;

        // 1.推力大小
        float thrustMagnitude = engine.power / relativeForwardSpeed.magnitude;
        // 2.推力方向
        Vector3 thrustDirection = transform.forward.normalized;
        // 3.推力
        Vector3 thrust = thrustDirection * thrustMagnitude;
        _thrust = thrustDirection * thrustMagnitude;

        // _combinedForce = _lift + _drag + _thrust;
        _combinedForce = _lift + _thrust + _drag;
        //1kg 1000N
        rb.AddForce(1000 * _combinedForce * Time.fixedDeltaTime);
    }
}