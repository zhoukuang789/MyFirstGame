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
}