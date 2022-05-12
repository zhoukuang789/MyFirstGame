using UnityEngine;

public class Killzone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        var plane = other.GetComponent<PlaneBehaviour>();
        if (plane == null)
        {
            var hc = other.GetComponent<PlaneHitCollider>();
            if (hc != null)
            {
                plane = hc.plane;
            }
        }
        if (plane != null)
            plane.health?.ReceiveDamage(int.MaxValue, PlaneHitCollider.PlaneHitPart.Main);
    }
}