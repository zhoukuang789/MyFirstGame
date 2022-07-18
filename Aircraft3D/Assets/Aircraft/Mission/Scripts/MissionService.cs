using System;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace Mission {
    public class MissionService : Singletonable<MissionService> {

        private MissionItem currentMission;

        /// <summary>
        /// 当前任务变化时发布的事件
        /// </summary>
        private event Action currentMissionChangedEvent;
        
        public MissionItem CreateMission() {
            MissionItem mission = new MissionItem();
            mission.SetStatus(MissionStatus.Ready);
            return mission;
        }
        
        public void Start(MissionItem missionItem) {
            if (missionItem.GetStatus() == MissionStatus.Processing)
                return;
            currentMission = missionItem;
            if (currentMissionChangedEvent != null) {
                currentMissionChangedEvent();
            }
            missionItem.GetOnStart()();
            missionItem.SetStatus(MissionStatus.Processing);
        }
        
        public void Complete(MissionItem missionItem) {
            if (missionItem.GetStatus() == MissionStatus.Completed)
                return;
            missionItem.GetOnComplete()();
            missionItem.SetStatus(MissionStatus.Completed);
        }

        public void Fail(MissionItem missionItem) {
            if (missionItem.GetStatus() == MissionStatus.Failed)
                return;
            missionItem.GetOnFail()();
            missionItem.SetStatus(MissionStatus.Failed);
        }

        public MissionItem GetCurrentMission() {
            return currentMission;
        }

        public void AddCurrentMissionChangedEventListener(Action action) {
            currentMissionChangedEvent += action;
        }
        
        public void RemoveCurrentMissionChangedEventListener(Action action) {
            currentMissionChangedEvent -= action;
        }

        public MissionPointBehaviour CreateMissionPoint(Transform transform, Action<Collider> onTrigger, bool isCanSee = true, bool isAutoDestroy = false) {
            GameObject missionPointPrefab = Resources.Load<GameObject>("Prefabs/MissionPoint");
            GameObject missionPoint = GameObject.Instantiate(missionPointPrefab, transform.position, transform.rotation);
            MissionPointBehaviour missionPointBehaviour = missionPoint.GetComponent<MissionPointBehaviour>();
            missionPointBehaviour.SetOnTriggerEnter(onTrigger);
            missionPointBehaviour.SetIsAutoDestroy(isAutoDestroy);
            if (!isCanSee) missionPoint.GetComponent<MeshRenderer>().enabled = false;
            missionPoint.SetActive(true);
            return missionPointBehaviour;
        }
    }
}