using UnityEngine;

public class ConfigService : MonoBehaviour {
    public static ConfigService instance { get; private set; }

    public PlanesConfig planesConfig;

    private void Awake() {
        instance = this;
    }
}