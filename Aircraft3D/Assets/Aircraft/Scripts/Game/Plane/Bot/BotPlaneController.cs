using System;
using UnityEngine;

public class BotPlaneController : PlaneController
{
    protected override void ReadPlaneInput()
    {
        //engine.AddPower();
        //engine.ReducePower();
    }

    public void AiControlPitch(float ratio)
    {
        DoPitch(ratio * plane.planeConfig.movement.pitchScaleFactor * Time.deltaTime);
    }

    public void AiControlRoll(float ratio)
    {
        DoRoll(ratio * plane.planeConfig.movement.rollScaleFactor * Time.deltaTime);
    }

    public void AiControlYaw(float ratio)
    {
        DoYaw(ratio * plane.planeConfig.movement.yawScaleFactor * Time.deltaTime);
    }
}