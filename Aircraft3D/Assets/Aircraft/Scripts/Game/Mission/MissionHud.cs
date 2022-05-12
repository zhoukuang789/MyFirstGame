using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 负责管理UI的任务显示
/// </summary>
public class MissionHud : MonoBehaviour
{
    public Text txt;
    public CanvasGroup cg;
    public static MissionHud instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        UpdateMission();
    }

    public void UpdateMission()
    {
        var item = MissionSystem.instance.currentMission;
        if (item==null)
        {
            Hide();
            //txt.text = "xxx";
            return;
        }

        cg.alpha = 1;
        var proto = MissionSystem.instance.GetMissionPrototype(item.id);
        txt.text = proto.desc + " <color=yellow>(" + item.quota + "/" + proto.missionCompleteQuota + ")</color>";
    }

    public void Hide()
    {
        cg.alpha = 0;
    }
}