using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedOne : MonoBehaviour {

    public PlayerController player;
    public Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        text.text = player.GetSpeedOne().ToString("0");
    }
}
