using System;
using UnityEngine;

public class PlaneMovement : MonoBehaviour {

    public Rigidbody rb;

    private PlaneConfig _planeConfig;
    
    // Start is called before the first frame update
    void Start() {
        
        // 获取飞机配置
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        
        // 质量
        rb.mass = _planeConfig.mass;
        // 初始速度
        rb.velocity = transform.forward * _planeConfig.movement.initialSpeed;

    }

    // Update is called once per frame
    void Update() {
    }

    private void FixedUpdate() {
        
    }

    /**
     * 通过高度获取大气密度
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