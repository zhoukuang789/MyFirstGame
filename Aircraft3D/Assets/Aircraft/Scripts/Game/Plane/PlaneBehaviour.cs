using UnityEngine;
using System.Collections;

public class PlaneBehaviour : MonoBehaviour
{
    public PlaneConfig planeConfig;

    [HideInInspector]
    public PlaneMovement movement;
    [HideInInspector]
    public PlaneWeapon weapon;
    [HideInInspector]
    public PlaneController controller;
    [HideInInspector]
    public PlaneEngine engine;

    protected virtual void Start()
    {
        movement = GetComponent<PlaneMovement>();
        weapon = GetComponent<PlaneWeapon>();
        controller = GetComponent<PlaneController>();
        engine = GetComponent<PlaneEngine>();

        movement.plane = this;
        weapon.plane = this;
        controller.plane = this;
        engine.plane = this;
    }
}
