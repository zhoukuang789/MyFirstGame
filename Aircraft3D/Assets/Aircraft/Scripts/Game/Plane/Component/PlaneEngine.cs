using UnityEngine;
using System.Collections;

public class PlaneEngine : PlaneComponent
{
    public float power { get; private set; }

    protected override void PostStart()
    {
        power = plane.planeConfig.movement.minPower;
    }

    public void AddPower()
    {
        power += plane.planeConfig.movement.addPowerSpeed * Time.deltaTime;
        power = Mathf.Clamp(power, plane.planeConfig.movement.minPower, plane.planeConfig.movement.maxPower);
    }

    public void ReducePower()
    {
        power -= plane.planeConfig.movement.reducePowerSpeed * Time.deltaTime;
        power = Mathf.Clamp(power, plane.planeConfig.movement.minPower, plane.planeConfig.movement.maxPower);
    }
}
