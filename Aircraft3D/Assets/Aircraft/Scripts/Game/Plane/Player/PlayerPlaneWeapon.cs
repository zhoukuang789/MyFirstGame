using UnityEngine;

public class PlayerPlaneWeapon : PlaneWeapon
{
    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            TryFire();
        }
    }
}