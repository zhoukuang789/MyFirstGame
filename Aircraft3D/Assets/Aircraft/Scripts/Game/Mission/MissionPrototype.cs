using UnityEngine;
using System;

/// <summary>
/// 单个任务的数据原型
/// </summary>
[CreateAssetMenu]
[System.Serializable]
public class MissionPrototype : ScriptableObject
{
    public string id;
    public string desc;

    public Action OnMissionFail;
    public Action OnMissionSuccess;

    public int missionCompleteQuota = 1;

    public MissionSystem.MissionCompleteTrigger completeTrigger;
}