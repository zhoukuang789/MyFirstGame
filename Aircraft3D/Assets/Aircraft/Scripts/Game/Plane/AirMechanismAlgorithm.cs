using UnityEngine;
using System;

public class AirMechanismAlgorithm
{
    /**
   * 通过海拔获取大气密度
   */
    public static float GetAirDensity(float altitude)
    {
        float airDensity = 1.225f;
        //return airDensity;

        float temperature;
        float airDensity0 = 1.225f;
        float temperature0 = 288.15f;
        if (altitude <= 11000f)
        {
            temperature = temperature0 - 0.0065f * altitude;
            airDensity = airDensity0 * (float)Math.Pow((temperature / temperature0), 4.25588);
        }
        else if (altitude > 11000f && altitude <= 20000f)
        {
            // temperature = 216.65f;
            airDensity = 0.36392f * (float)Math.Exp((-altitude + 11000) / 6341.62);
        }
        else if (altitude > 20000)
        {
            temperature = 216.65f + 0.001f * (altitude - 20000f);
            airDensity = 0.088035f * (float)Math.Pow((temperature / 216.65f), -35.1632);
        }

        return airDensity;
    }

    public static Vector3 GetLift(float airDensity, float attackOfAngle, Vector3 currentSpeed, Vector3 right, Vector3 forward, PlaneConfig cfg)
    {
        var relativeForwardSpeed = Vector3.Project(currentSpeed, forward);
        // 4.升力系数
        float liftCoefficient = cfg.movement.liftCoefficientCurve.Evaluate(Mathf.Clamp(attackOfAngle, -10, 25));
        // ==>5.升力大小
        float liftMagnitude = 0.5f * airDensity * relativeForwardSpeed.magnitude * relativeForwardSpeed.magnitude * cfg.movement.wingArea * liftCoefficient;
        // 6.升力方向
        Vector3 liftDirection = Vector3.Cross(-right, currentSpeed).normalized;
        // 7.升力
        //TODO 目前的唯一的问题是升力偏大了，大约50倍左右，其他问题基本解决了
        var lift = liftDirection * liftMagnitude * 0.02f;
        //Debug.Log("relativeForwardSpeed " + relativeForwardSpeed.magnitude + "\nattackOfAngle " + attackOfAngle + "\nliftCoefficient " + liftCoefficient + "\nliftMagnitude " + liftMagnitude);
        return lift;
    }

    public static Vector3 GetDrag(float airDensity, Vector3 currentSpeed, float attackOfAngle, Vector3 forward, PlaneConfig cfg)
    {
        var relativeForwardSpeed = Vector3.Project(currentSpeed, forward);
        // 1.阻力系数
        float dragCoefficient = cfg.movement.dragCoefficientCurve.Evaluate(Mathf.Clamp(attackOfAngle, -10, 19));
        // 2.阻力大小
        float dragMagnitude = 0.5f * airDensity * relativeForwardSpeed.magnitude * relativeForwardSpeed.magnitude * cfg.movement.wingArea * dragCoefficient;
        // 3.阻力方向
        Vector3 dragDirection = -currentSpeed.normalized;
        // 4.阻力
        var drag = dragDirection * dragMagnitude;
        return drag;
    }

    public static Vector3 GetThrust(Vector3 currentSpeed, Vector3 forward, float power)
    {
        var relativeForwardSpeed = Vector3.Project(currentSpeed, forward);
        // 1.推力大小
        float thrustMagnitude = power / relativeForwardSpeed.magnitude;
        // 2.推力方向
        Vector3 thrustDirection = forward.normalized;
        // 3.推力
        var thrust = thrustDirection * thrustMagnitude;
        return thrust;
    }
}