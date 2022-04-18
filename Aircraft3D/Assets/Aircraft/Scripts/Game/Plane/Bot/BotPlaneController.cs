using System;
using UnityEngine;

public class BotPlaneController : PlaneController
{
    public enum AIControlLevel
    {
        None,

        Positive_100,
        Positive_75,
        Positive_50,
        Positive_25,

        Negative_100,
        Negative_75,
        Negative_50,
        Negative_25,
    }

    protected override void ReadPlaneInput()
    {
        //engine.AddPower();
        //engine.ReducePower();
    }

    float GetValueByAiControlLevel(AIControlLevel lv)
    {
        switch (lv)
        {
            case AIControlLevel.Negative_100:
                return -1;

            case AIControlLevel.Negative_75:
                return -0.75f;

            case AIControlLevel.Negative_50:
                return -0.5f;

            case AIControlLevel.Negative_25:
                return -0.25f;

            case AIControlLevel.Positive_100:
                return 1;

            case AIControlLevel.Positive_75:
                return 0.75f;

            case AIControlLevel.Positive_50:
                return 0.5f;

            case AIControlLevel.Positive_25:
                return 0.25f;
        }

        return 0;
    }

    public void AiControlPitch(AIControlLevel level)
    {
        DoPitch(GetValueByAiControlLevel(level) * plane.planeConfig.movement.pitchScaleFactor * Time.deltaTime);
    }

    public void AiControlRoll(AIControlLevel level)
    {
        DoRoll(GetValueByAiControlLevel(level) * plane.planeConfig.movement.rollScaleFactor * Time.deltaTime);
    }

    public void AiControlYaw(AIControlLevel level)
    {
        DoYaw(GetValueByAiControlLevel(level) * plane.planeConfig.movement.yawScaleFactor * Time.deltaTime);
    }
}