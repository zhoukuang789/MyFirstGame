using Airplane.Movement;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOne : MonoBehaviour {

    public Airplane.PlaneBehaviour playerPlane;
    public Text text;

    // Update is called once per frame
    void Update() {
        text.text = PlaneMovementControllerService.GetInstance().SetPlane(playerPlane).GetSpeed().ToString("0");
    }
}
