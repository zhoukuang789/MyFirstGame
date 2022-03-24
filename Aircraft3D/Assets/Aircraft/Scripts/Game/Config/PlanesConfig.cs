using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class PlanesConfig : ScriptableObject
{
    public List<PlaneConfig> planesConfigs;

    public float G = 9.81f;

    public PlaneConfig PlayerPlaneConfig
    {
        get
        {
            return planesConfigs[0];
        }
    }
}