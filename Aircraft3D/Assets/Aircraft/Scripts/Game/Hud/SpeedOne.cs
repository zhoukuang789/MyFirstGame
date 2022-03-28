using UnityEngine;
using UnityEngine.UI;

public class SpeedOne : MonoBehaviour {

    public PlaneController player;
    public Text text;

    // Update is called once per frame
    void Update() {
        text.text = player.MySpeed.ToString("0");
    }
}
