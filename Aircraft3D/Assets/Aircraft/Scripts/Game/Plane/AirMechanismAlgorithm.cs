using UnityEngine;
using System;

public class AirMechanismAlgorithm {
    /**
   * 通过海拔获取大气密度
   */
    public static float GetAirDensity(float altitude) {
        float airDensity = 1.225f;
        //return airDensity;

        float temperature;
        float airDensity0 = 1.225f;
        float temperature0 = 288.15f;
        if (altitude <= 11000f) {
            temperature = temperature0 - 0.0065f * altitude;
            airDensity = airDensity0 * (float) Math.Pow((temperature / temperature0), 4.25588);
        } else if (altitude > 11000f && altitude <= 20000f) {
            // temperature = 216.65f;
            airDensity = 0.36392f * (float) Math.Exp((-altitude + 11000) / 6341.62);
        } else if (altitude > 20000) {
            temperature = 216.65f + 0.001f * (altitude - 20000f);
            airDensity = 0.088035f * (float) Math.Pow((temperature / 216.65f), -35.1632);
        }

        return airDensity;
    }

    // 迎角
    public static float GetAoa(Vector3 velocity, Vector3 transformUp, Vector3 transformForward) {
        float aoa = 0f;
        if (Vector3.Angle(velocity, transformUp) < 90) {
            aoa = -Vector3.Angle(velocity, transformForward);
        } else {
            aoa = Vector3.Angle(velocity, transformForward);
        }
        // Mathf.Clamp(aoa, -18f, 25f);
        return aoa;
    }
    
    // 侧滑角
    public static float GetSideAngle(Vector3 velocity, Vector3 transformRight, Vector3 transformForward) {
        float sideAngle = 0f;
        if (Vector3.Angle(velocity, transformRight) < 90) {
            sideAngle = -Vector3.Angle(velocity, transformForward);
        } else {
            sideAngle = Vector3.Angle(velocity, transformForward);
        }
        return sideAngle;
    }

    // 升力
    public static Vector3 GetLift(float airDensity, Vector3 velocity, PlaneConfig planeConfig, float aoa, Vector3 transformRight) {
        float liftMagnitude = 0.5f * airDensity * velocity.sqrMagnitude * planeConfig.movement.wingArea * planeConfig.movement.liftCoefficientCurve.Evaluate(aoa);
        Vector3 liftDirection = Vector3.Cross(velocity, transformRight).normalized;
        Vector3 lift = liftDirection * liftMagnitude;
        return lift;
    }

    // 阻力
    public static Vector3 GetDrag(float airDensity, Vector3 velocity, PlaneConfig planeConfig, float aoa) {
        float dragMagnitude = 0.5f * airDensity * velocity.sqrMagnitude * planeConfig.movement.wingArea * planeConfig.movement.dragCoefficientCurve.Evaluate(aoa);
        Vector3 dragDirection = -velocity.normalized;
        Vector3 drag = dragDirection * dragMagnitude;
        return drag;
    }

    // 推力
    public static Vector3 GetThrust(Vector3 velocity, Vector3 transformForward, float power) {
        float thrustMagnitude = power / velocity.magnitude;
        Vector3 thrustDirection = transformForward;
        Vector3 thrust = thrustDirection * thrustMagnitude;
        return thrust;
    }
}