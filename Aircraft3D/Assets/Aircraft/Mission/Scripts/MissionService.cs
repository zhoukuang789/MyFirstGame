using ProjectBase.SingletonBase;

namespace Mission {
    public class MissionService : Singletonable<MissionService> {

        public MissionItem CreateMission() {
            return new MissionItem();
        }
        
        public void Start(MissionItem missionItem) {
            missionItem.GetOnStart()();
            missionItem.SetStatus(MissionStatus.Processing);
        }
        
    }
}