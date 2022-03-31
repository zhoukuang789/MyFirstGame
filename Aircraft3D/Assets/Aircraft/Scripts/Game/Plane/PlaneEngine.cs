using UnityEngine;
using System.Collections;

public class PlaneEngine : MonoBehaviour
{
    public float power { get; private set; }

    public float addPowerSpeed = 2000;
    public float reducePowerSpeed = 2000;

    private PlaneConfig _planeConfig;

    private void Start()
    {
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
    }

    public void AddPower()
    {
        power += addPowerSpeed * Time.deltaTime;
        power = Mathf.Clamp(power, 0, _planeConfig.movement.maxPower);
    }

    public void ReducePower()
    {
        power -= reducePowerSpeed * Time.deltaTime;
        power = Mathf.Clamp(power, 0, _planeConfig.movement.maxPower);
    }
}
