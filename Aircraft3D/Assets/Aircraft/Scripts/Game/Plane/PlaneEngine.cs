using UnityEngine;
using System.Collections;

public class PlaneEngine : MonoBehaviour
{
    public float power { get; private set; }

    private PlaneConfig _planeConfig;

    private void Start()
    {
        _planeConfig = ConfigService.instance.planesConfig.PlayerPlaneConfig;
        power = _planeConfig.movement.minPower;
    }

    public void AddPower()
    {
        power += _planeConfig.movement.addPowerSpeed * Time.deltaTime;
        power = Mathf.Clamp(power, _planeConfig.movement.minPower, _planeConfig.movement.maxPower);
    }

    public void ReducePower()
    {
        power -= _planeConfig.movement.reducePowerSpeed * Time.deltaTime;
        power = Mathf.Clamp(power, _planeConfig.movement.minPower, _planeConfig.movement.maxPower);
    }
}
