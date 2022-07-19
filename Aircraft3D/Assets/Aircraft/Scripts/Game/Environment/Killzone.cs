using Airplane.Health;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        
        Airplane.PlaneBehaviour plane = other.gameObject.GetComponentInParent<Airplane.PlaneBehaviour>();
        if (plane != null && plane.controller == Airplane.PlaneController.Player) {
            PlaneHealthService.GetInstance().SetPlane(plane).ReceiveDamage(100f);
        }
    }
}