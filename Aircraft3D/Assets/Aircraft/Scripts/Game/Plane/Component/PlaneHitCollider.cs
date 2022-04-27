using UnityEngine;
using System.Collections;

public class PlaneHitCollider : PlaneComponent
{
    public enum PlaneHitPart
    {
        Other,
        LeftWing,
        RightWing,
        Main,
        BackWing
    }

    public PlaneHitPart part;
}
