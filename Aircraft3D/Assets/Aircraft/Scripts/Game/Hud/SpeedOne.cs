using Plane.Movement;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOne : MonoBehaviour {

    public Plane.PlaneBehaviour playerPlane;
    public Text text;

    // Update is called once per frame
    void Update() {
        text.text = PlaneMovementControllerService.GetInstance().SetPlane(playerPlane).GetSpeed().ToString("0");
    }
}
