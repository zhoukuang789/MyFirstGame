using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class PlanesConfig : ScriptableObject {
    
    public List<PlaneConfig> planesConfigs;

    public PlaneConfig PlayerPlaneConfig {
        get { return planesConfigs[0]; }
    }
}