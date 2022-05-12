using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 所有任务的设置
/// </summary>
[CreateAssetMenu]
public class MissionsConfig : ScriptableObject
{
    public List<MissionPrototype> missions;
}
