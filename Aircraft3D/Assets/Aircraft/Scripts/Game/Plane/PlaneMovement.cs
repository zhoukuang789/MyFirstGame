using System;
using UnityEngine;

public class PlaneMovement : MonoBehaviour {

    public Rigidbody rb;

    private PlaneConfig _planeConfig;
    
    // Start is called before the first frame update
    void Start() {
        
        // 获取飞机配置
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        
        // 设置刚体质量
        rb.mass = _planeConfig.mass;
        // 设置刚体初始速度
        rb.velocity = transform.forward * _planeConfig.movement.initialSpeed;

    }

    // Update is called once per frame
    void Update() {
    }

    private void FixedUpdate() {
        // 1.空气密度
        float airDensity = GetAirDensity(transform.position.y);
        // 2.飞机速度大小
        float speed = rb.velocity.magnitude;
        // 3.飞机迎角
        float attackOfAngle = Vector3.Angle(transform.forward, Vector3.ProjectOnPlane(rb.velocity, transform.right));
        // 4.升力系数
        float liftCoefficient = _planeConfig.movement.liftCoefficientCurve.Evaluate(attackOfAngle);
        // ==>5.升力大小
        float liftMagnitude = 0.5f * airDensity * speed * speed * _planeConfig.movement.wingArea * liftCoefficient;
        // 6.升力方向
        Vector3 liftDirection = -Vector3.Cross(transform.right, rb.velocity).normalized;
        // 7.升力
        Vector3 lift = liftDirection * liftMagnitude;
        
        
        // 1.阻力系数
        float dragCoefficient = _planeConfig.movement.dragCoefficientCurve.Evaluate(attackOfAngle);
        // 2.阻力大小
        float dragMagnitude = 0.5f * airDensity * speed * speed * _planeConfig.movement.wingArea * dragCoefficient;
        // 3.阻力方向
        Vector3 dragDirection = -transform.forward.normalized;
        // 4.阻力
        Vector3 drag = dragDirection * dragMagnitude;
        
        // 1.推力大小
        float thrustMagnitude = _planeConfig.movement.powerOne / speed;
        // 2.推力方向
        Vector3 thrustDirection = transform.forward.normalized;
        // 3.推力
        Vector3 thrust = thrustDirection * thrustMagnitude;
        
        rb.AddForce((lift + drag + thrust) * Time.fixedDeltaTime);
    }

    /**
     * 通过海拔获取大气密度
     */
    private static float GetAirDensity(float altitude) {
        float airDensity = 1.225f;
        float temperature;
        float airDensity0 = 1.225f;
        float temperature0 = 288.15f;
        if (altitude <= 11000f) {
            temperature = temperature0 - 0.0065f * altitude;
            airDensity = airDensity0 * (float) Math.Pow((temperature / temperature0), 4.25588);
        } else if (altitude > 11000f && altitude <= 20000f) {
            // temperature = 216.65f;
            airDensity = 0.36392f * (float)Math.Exp((-altitude + 11000) / 6341.62);
        } else if (altitude > 20000) {
            temperature = 216.65f + 0.001f * (altitude - 20000f);
            airDensity = 0.088035f * (float) Math.Pow((temperature / 216.65f), -35.1632);
        }
        
        return airDensity;
    }
    
    
    
    
}