using System;
using System.Collections.Generic;
using Dialog.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Mission {
    public class MissionBehaviour : MonoBehaviour {
        public List<Transform> missionPositionList;
        public GameObject missionPointPrefab;
        private GameObject missionPoint;
        private int i = 0;
        private MissionItem missionItem;


        private void Start() {
            //创建一个任务：在敌军摧毁所有目标之前销毁三架轰炸机
            missionItem = MissionService.GetInstance().CreateMission();
            missionItem
                .SetName("关卡2任务")
                .SetDescription("经过路径点")
                .SetTotalProgress(3)
                .SetOnStart(() => {
                    missionPoint = Instantiate(missionPointPrefab);
                    if (i == missionPositionList.Count - 1) {
                        missionPoint.GetComponent<MissionPointBehaviour>()
                            .SetPosition(missionPositionList[i].position)
                            .SetOnTriggerEnter(triggerEnter);
                    }

                    if (i != missionPositionList.Count - 1) {
                        missionPoint.GetComponent<MissionPointBehaviour>()
                            .SetPosition(missionPositionList[i].position)
                            .SetNextPosition(missionPositionList[i + 1].position)
                            .SetOnTriggerEnter(triggerEnter);
                        i++;
                    }
                })
                .SetOnFail(() => {
                    // 弹窗
                })
                .SetOnComplete(() => {
                    // 下一关
                    List<ButtonData> buttonList = new List<ButtonData>();
                    buttonList.Add(new ButtonData("Quit", () => { SceneManager.LoadScene(0); }));
                    // DialogService.GetInstance().ShowMenu("mission complete", buttonList);
                })
                .SetStatus(MissionStatus.Ready);

            MissionService.GetInstance().Start(missionItem);
        }

        private void triggerEnter(Collider other) {
            
            
            if (i == missionPositionList.Count - 1) {
                missionItem.UpdateCurrentProgress();
                missionPoint.GetComponent<MissionPointBehaviour>()
                    .SetPosition(missionPositionList[i].position)
                    .SetIsLastPosition(true)
                    .SetOnTriggerEnter((other) => { missionItem.GetOnComplete()(); });
            }

            if (i != missionPositionList.Count - 1) {
                missionItem.UpdateCurrentProgress();
                missionPoint.GetComponent<MissionPointBehaviour>()
                    .SetPosition(missionPositionList[i].position)
                    .SetNextPosition(missionPositionList[i + 1].position)
                    .SetOnTriggerEnter(triggerEnter);
                i++;
            }
        }
    }
}