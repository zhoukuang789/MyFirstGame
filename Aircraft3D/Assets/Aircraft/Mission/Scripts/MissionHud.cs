using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mission {

    /// <summary>
    /// 负责管理UI的任务显示
    /// </summary>
    public class MissionHud : MonoBehaviour {
        
        public Text txt;
        public CanvasGroup cg;

        private MissionItem currentMission;

        private void Awake() {
            txt = GetComponentInChildren<Text>();
            cg = GetComponent<CanvasGroup>();
            MissionService.GetInstance().AddCurrentMissionChangedEventListener(CurrentMissionChanged);
        }

        private void OnDestroy() {
            MissionService.GetInstance().RemoveCurrentMissionChangedEventListener(CurrentMissionChanged);
        }

        private void CurrentMissionChanged() {
            currentMission = MissionService.GetInstance().GetCurrentMission();
            UpdateMission();
            currentMission.AddProgressUpdateEventListener(UpdateMission);
            currentMission.SetOnComplete(() => {
                currentMission.RemoveProgressUpdateEventListener(UpdateMission);
            });
            currentMission.SetOnFail(() => {
                currentMission.RemoveProgressUpdateEventListener(UpdateMission);
            });
        }

        private void UpdateMission() {
            txt.text = currentMission.GetDescription() + " <color=yellow>(" + currentMission.GetCurrentProgress() + "/" + currentMission.GetTotalProgress() + ")</color>";
        }

        public void Hide() {
            cg.alpha = 0;
        }
    }
}