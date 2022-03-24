using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static float G = 9.81f;
    
    public Rigidbody rb;
    public ConstantForce cf;

    public float initialSpeed = 300f;       // 飞机初始速度
    public float maxSpeed = 500f;           // 飞机最大飞行速度
    public float minSpeed = 200f;           // 飞机最小飞行速度
    public float stayFlySpeed = 220f;       // 飞机维持升力速度
    public float acceleration = 40f;        // 按住W时的飞机加速度
    public float deceleration = 20f;        // 按住S时的飞机减速度
    public float naturalAcceleration = 15f; // 不按任何按键时的飞机加速度
    public float naturalDeceleration = 10f; // 不按任何按键时的飞机减速度

    public AnimationCurve pitchRateCurve;   // 飞机俯仰速率曲线
    public float pitchScaleFactor = 45f;    // 飞机俯仰比例系数
    public AnimationCurve rollRateCurve;    // 飞机滚转速率曲线
    public float rollScaleFactor = 90f;     // 飞机滚转比例系数
    public AnimationCurve yawRateCurve;     // 飞机偏航速率曲线
    public float yawScaleFactor = 20f;      // 飞机偏航比例系数
    
    private void Start() {
        SetSpeedOne(initialSpeed);
        cf.force = transform.forward.normalized * rb.mass * G;
    }
    
    private void Update() {
        // 按住W加速，如果速度超过最大飞行速度不能再加速
        if (Input.GetKey(KeyCode.W) && GetSpeedOne() <= maxSpeed) {
            SetSpeedOne(GetSpeedOne() + acceleration * Time.deltaTime);
        }

        // 按住S减速，如果速度小于最小飞行速度不能再减速
        if (Input.GetKey(KeyCode.S) && GetSpeedOne() > minSpeed) {
            SetSpeedOne(GetSpeedOne() - deceleration * Time.deltaTime);
        }
        
        // 不按加速减速按键时，如果飞机速度大于初始速度，飞机会按自然减速度进行减速，直到减到初始速度
        if (GetSpeedOne() > initialSpeed && !Input.GetKey((KeyCode.W)) && !Input.GetKey(KeyCode.S)) {
            SetSpeedOne(GetSpeedOne() - naturalDeceleration * Time.deltaTime);
        }
        
        // 不按加速减速按键时，如果飞机速度小于初始速度，飞机会按自然加速度进行加速，直到减到初始速度
        if (GetSpeedOne() < initialSpeed && !Input.GetKey((KeyCode.W)) && !Input.GetKey(KeyCode.S)) {
            SetSpeedOne(GetSpeedOne() + naturalAcceleration * Time.deltaTime);
        }
        
        // 按下空格键或鼠标Y轴增大，飞机向上俯仰
        if (Input.GetKey(KeyCode.Space) || Input.GetAxis("Mouse Y") > 0) {
            Pitch(pitchRateCurve.Evaluate(GetSpeedOne()/maxSpeed) * pitchScaleFactor * Time.deltaTime);
        }
        
        // 按下Alt或鼠标Y轴减小，飞机向下俯仰
        if (Input.GetKey(KeyCode.AltGr) || Input.GetAxis("Mouse Y") < 0) {
            Pitch(-pitchRateCurve.Evaluate(GetSpeedOne()/maxSpeed) * pitchScaleFactor * Time.deltaTime);
        }
        
        // 鼠标X轴增大，飞机向右滚转
        if (Input.GetAxis("Mouse X") > 0) {
            Roll(rollRateCurve.Evaluate(GetSpeedOne()/maxSpeed) * rollScaleFactor * Time.deltaTime);
        }
        
        // 鼠标X轴减小，飞机向左滚转
        if (Input.GetAxis("Mouse X") < 0) {
            Roll(-rollRateCurve.Evaluate(GetSpeedOne()/maxSpeed) * rollScaleFactor * Time.deltaTime);
        }
        
        // 按下D键，飞机右偏航
        if (Input.GetKey(KeyCode.D)) {
            Yaw(yawRateCurve.Evaluate(GetSpeedOne()/maxSpeed) * yawScaleFactor * Time.deltaTime);
        }
        
        // 按下A键，飞机左偏航
        if (Input.GetKey(KeyCode.A)) {
            Yaw(-yawRateCurve.Evaluate(GetSpeedOne()/maxSpeed) * yawScaleFactor * Time.deltaTime);
        }
        
    }

    private void FixedUpdate() {
        // 如果飞机速度小于维持升力速度，将受到重力
        if (GetSpeedOne() < stayFlySpeed) {
            cf.enabled = false;
        } else {
            cf.enabled = true;
        }
    }

    /**
     * 获取飞机实时速度
     */
    public float GetSpeedOne() {
        return rb.velocity.magnitude;
    }

    /**
     * 设置飞机实时速度
     */
    public void SetSpeedOne(float speed) {
        rb.velocity = transform.forward.normalized * speed;
    }
    
    /**
     * 俯仰操作
     */
    private void Pitch(float angle) {
        Quaternion quaternion = Quaternion.AngleAxis(angle, -transform.right);
        transform.rotation = quaternion * transform.rotation;
    }
    
    /**
     * 滚转操作
     */
    private void Roll(float angle) {
        Quaternion quaternion = Quaternion.AngleAxis(angle, -transform.forward);
        transform.rotation = quaternion * transform.rotation;
    }
    
    /**
     * 偏航操作
     */
    private void Yaw(float angle) {
        Quaternion quaternion = Quaternion.AngleAxis(angle, transform.up);
        transform.rotation = quaternion * transform.rotation;
    }
}