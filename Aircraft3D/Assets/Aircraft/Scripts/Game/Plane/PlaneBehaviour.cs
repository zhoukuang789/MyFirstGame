using UnityEngine;
using System.Collections.Generic;

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
    [HideInInspector]
    public PlaneHealth health;

    public List<PlaneHitCollider> hitColliders;

    protected virtual void Start()
    {
        movement = GetComponent<PlaneMovement>();
        weapon = GetComponent<PlaneWeapon>();
        controller = GetComponent<PlaneController>();
        engine = GetComponent<PlaneEngine>();
        health = GetComponent<PlaneHealth>();

        movement.plane = this;
        weapon.plane = this;
        controller.plane = this;
        engine.plane = this;
        health.plane = this;

        foreach (var hc in hitColliders)
        {
            hc.plane = this;
        }
    }
}
