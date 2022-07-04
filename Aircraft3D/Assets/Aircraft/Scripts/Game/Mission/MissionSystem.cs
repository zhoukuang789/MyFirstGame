using UnityEngine;
using System.Collections;

public class MissionSystem : MonoBehaviour
{
    public MissionsConfig missionsConfig;

    public MissionItem currentMission;

    public static MissionSystem instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetNewMission();
    }

    public enum MissionCompleteTrigger
    {
        DestroyEnemyBomer,
        EnterRallySpot,
        DestroyEnemyCruiser,
    }

    public MissionPrototype GetMissionPrototype(string id)
    {
        foreach (var missionProto in missionsConfig.missions)
        {
            if (missionProto.id == id)
            {
                return missionProto;
            }
        }

        return null;
    }

    public void SetNewMission()
    {
        var proto = missionsConfig.missions[0];
        currentMission = new MissionItem();
        currentMission.quota = 0;
        currentMission.id = proto.id;

        // MissionHud.instance.UpdateMission();
    }

    public void TryPushforwardMission(MissionCompleteTrigger trigger)
    {
        if (GetMissionPrototype(currentMission.id).completeTrigger == trigger)
        {
            currentMission.quota++;
            //CheckMissionComplete();
        }
    }
}
