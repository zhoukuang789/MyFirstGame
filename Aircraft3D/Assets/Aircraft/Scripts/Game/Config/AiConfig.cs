using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class AiConfig : ScriptableObject
{
    public float openFireAngleMax = 20;
    public float openFireRangeMax = 400;

    public float bomberReleaseBombHorizontalDistance=40;
    public float bomberReleaseBombIdealHeight = 100;
}