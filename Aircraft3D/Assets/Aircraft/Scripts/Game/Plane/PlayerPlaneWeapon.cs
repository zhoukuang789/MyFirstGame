using UnityEngine;

public class PlayerPlaneWeapon : PlaneBasicWeapon
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }
}