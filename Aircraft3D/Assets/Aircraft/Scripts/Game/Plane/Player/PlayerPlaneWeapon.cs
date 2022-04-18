using UnityEngine;

public class PlayerPlaneWeapon : PlaneWeapon
{
    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }
}