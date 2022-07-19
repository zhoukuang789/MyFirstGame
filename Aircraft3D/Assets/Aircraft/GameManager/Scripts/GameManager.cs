using System;
using System.Collections.Generic;
using Airplane;
using Mission;
using UnityEngine;
using System.Collections;
using Airplane.Movement;
using Airplane.Weapon;
using com;
using DG.Tweening;
using Dialog.Scripts;
using MyCamera;
using UnityEngine.SceneManagement;
using InputService = ProjectBase.Input.InputService;

namespace GameManager {
    public enum LevelNumber {
        Level1,
        Level2,
        Level3
    }

    public class GameManager : MonoBehaviour {
        public LevelNumber levelNumber;

        public CameraBehaviour mainCamera;

        public Transform playerPlaneSpawnTransform;

        private GameObject playerPlane;

        [Header("Level 1")]
        public Transform enemyBomberSpawnTransformInLevel1;

        public Transform missionPointTransform;
        public Transform transParam1;
        public Transform transParam2;
        public Transform transParam3;
        public Transform transParam4;
        public Transform transParam5;
        private Mission.MissionItem mission1;
        private int currentKillBomberNumber;
        private MissionPointBehaviour mission1Point;

        private List<GameObject> enemyBomberList;

        [Header("Level 2")]
        public Transform enemyBomberSpawnTransformInLevel2;
        public Transform transParamII1;
        public Transform transParamII2;
        public Transform transParamII3;
        public Transform transParamII4;
        public Transform transParamII5;
        public Transform firstMissionPoint;
        public Transform level2MissionPoints;
        private List<Transform> level2MissionPointList;
        private int cursor;
        private Mission.MissionItem mission2;

        private void Start() {
            // 生成玩家飞机
            playerPlane = PlaneFactory.GetInstance().CreatePlayerPlane(playerPlaneSpawnTransform.position,
                playerPlaneSpawnTransform.rotation);
            StartCoroutine("StartMissions");
        }

        IEnumerator StartMissions() {
            yield return new WaitForSeconds(0.1f);
            switch (levelNumber) {
                case LevelNumber.Level1:
                    Level1Init();
                    break;
                case LevelNumber.Level2:
                    Level2Init();
                    break;
                default:
                    break;
            }

            yield return null;
        }

        private void Level1Init() {
            // 生成敌军飞机
            enemyBomberList = new List<GameObject>();
            GameObject enemyBomber1 = PlaneFactory.GetInstance()
                .CreateEnemyBomber(enemyBomberSpawnTransformInLevel1.position,
                    enemyBomberSpawnTransformInLevel1.rotation);
            GameObject enemyBomber2 = PlaneFactory.GetInstance().CreateEnemyBomber(
                enemyBomberSpawnTransformInLevel1.position + new Vector3(0f, 0f, 50f),
                enemyBomberSpawnTransformInLevel1.rotation);
            GameObject enemyBomber3 = PlaneFactory.GetInstance().CreateEnemyBomber(
                enemyBomberSpawnTransformInLevel1.position + new Vector3(0f, 0f, -50f),
                enemyBomberSpawnTransformInLevel1.rotation);
            enemyBomberList.Add(enemyBomber1);
            enemyBomberList.Add(enemyBomber2);
            enemyBomberList.Add(enemyBomber3);
            

            // 创建关卡1任务
            mission1 = MissionService.GetInstance().CreateMission()
                .SetName("任务1")
                .SetDescription("Protect allied buildings from being destroyed and shoot down at least 1 enemy bomber.")
                .SetTotalProgress(1)
                .SetOnStart(() => {
                    // 任务开始时
                    // 记下当前击杀轰炸机的数量
                    currentKillBomberNumber = Record.RecordService.GetInstance().GetKillRecord(PlaneType.Bomber);
                    Record.RecordService.GetInstance().AddKillRecordChangeEventListener(KillRecordChanged);
                    // 生成任务点
                    mission1Point = MissionService.GetInstance()
                        .CreateMissionPoint(missionPointTransform, Mission1PointEnter, false);
                })
                .SetOnComplete(() => {
                    // 任务完成时
                    // 禁用输入
                    InputService.GetInstance().CloseInput();
                    // 显示完成菜单
                    MenuBehaviour completeMenu = DialogService.GetInstance().CreateMenu("Mission\nComplete", null, false);
                    completeMenu.AddButton("Next", () => {
                        SceneManager.LoadScene(2);
                        Destroy(completeMenu.gameObject);
                    });
                    completeMenu.Show();
                })
                .SetOnFail(() => {
                    // 任务失败时执行
                    // 禁用输入
                    InputService.GetInstance().CloseInput();
                    // 显示失败菜单
                    MenuBehaviour failMenu = DialogService.GetInstance().CreateMenu("mission failed");
                    failMenu.AddButton("Retry", () => {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        Destroy(failMenu.gameObject);
                    });
                    failMenu.AddButton("Quit", () => {
                        SceneManager.LoadScene(0);
                        Destroy(failMenu.gameObject);
                    });
                    failMenu.Show();
                });
            MissionService.GetInstance().Start(mission1);
        }

        /// <summary>
        /// 进入任务点时
        /// </summary>
        /// <param name="other"></param>
        private void Mission1PointEnter(Collider other) {
            Airplane.PlaneBehaviour plane = other.gameObject.GetComponentInParent<Airplane.PlaneBehaviour>();
            if (plane != null && plane.controller == Airplane.PlaneController.Player) {
                Airplane.PlaneBehaviour playerPlaneBehaviour = playerPlane.GetComponent<Airplane.PlaneBehaviour>();
                // 飞机停止动作
                PlaneMovementControllerService.GetInstance().SetPlane(playerPlaneBehaviour).StopPlane();

                // 镜头动画
                CameraService.GetInstance().SpotTrack(transParam2.position, transParam3, 3f, () => {
                    GameObject enemyBomber = enemyBomberList[0];
                    CameraService.GetInstance().SpotTrack(transParam1.position, enemyBomber.transform, 5f, () => {
                        PlaneMovementControllerService.GetInstance().SetPlane(playerPlaneBehaviour).RestorePlane();
                        DialogService.GetInstance().Hint("Protect allied buildings from being destroyed and shoot down at least 1 enemy bomber.");
                    });
                });
                transParam3.DOMove(transParam4.position, 1.5f).OnComplete(() => {
                    transParam3.DOMove(transParam5.position, 1.5f);
                });
                
                Destroy(mission1Point.gameObject);
            }
        }

        /// <summary>
        /// 击杀记录改变时执行
        /// </summary>
        /// <param name="map"></param>
        private void KillRecordChanged(Dictionary<PlaneType, int> map) {
            if (map[PlaneType.Bomber] - currentKillBomberNumber > 0) {
                mission1.UpdateCurrentProgress();
                if (mission1.GetCurrentProgress() == mission1.GetTotalProgress()) {
                    MissionService.GetInstance().Complete(mission1);
                }
            }
        }

        private void Level2Init() {
            level2MissionPointList = new List<Transform>();
            foreach (Transform missionPoint in level2MissionPoints.transform) {
                level2MissionPointList.Add(missionPoint);
            }
            cursor = 0;
            
            // 生成敌军飞机
            enemyBomberList = new List<GameObject>();
            GameObject enemyBomber1 = PlaneFactory.GetInstance()
                .CreateBackEnemyBomber(enemyBomberSpawnTransformInLevel2.position,
                    enemyBomberSpawnTransformInLevel2.rotation);
            GameObject enemyBomber2 = PlaneFactory.GetInstance().CreateBackEnemyBomber(
                enemyBomberSpawnTransformInLevel2.position + new Vector3(100f, 0, 0f),
                enemyBomberSpawnTransformInLevel2.rotation);
            enemyBomberList.Add(enemyBomber1);
            enemyBomberList.Add(enemyBomber2);

            // 创建关卡2任务
            mission2 = MissionService.GetInstance().CreateMission()
                .SetName("任务2")
                .SetDescription("Take advantage of the victory and pursue the retreating enemy bombers.")
                .SetTotalProgress(7)
                .SetOnStart(() => {
                    // 任务开始时
                    // 生成任务点
                    mission1Point = MissionService.GetInstance().CreateMissionPoint(missionPointTransform, Mission2PointEnter, false);
                    
                })
                .SetOnComplete(() => {
                    // 任务完成时
                    // 禁用输入
                    InputService.GetInstance().CloseInput();
                    // 显示完成菜单
                    MenuBehaviour completeMenu = DialogService.GetInstance().CreateMenu("mission\ncomplete");
                    completeMenu.AddButton("Main Menu", () => {
                        SceneManager.LoadScene(0);
                        Destroy(completeMenu.gameObject);
                    });
                    completeMenu.Show();
                })
                .SetOnFail(() => {
                    // 任务失败时执行
                    // 禁用输入
                    InputService.GetInstance().CloseInput();
                    // 显示失败菜单
                    MenuBehaviour failMenu = DialogService.GetInstance().CreateMenu("mission\nfailed");
                    failMenu.AddButton("Retry", () => {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        Destroy(failMenu.gameObject);
                    });
                    failMenu.AddButton("Quit", () => {
                        SceneManager.LoadScene(0);
                        Destroy(failMenu.gameObject);
                    });
                    failMenu.Show();
                });
            MissionService.GetInstance().Start(mission2);
        }
        
        private void Mission2PointEnter(Collider other) {
            Airplane.PlaneBehaviour plane = other.gameObject.GetComponentInParent<Airplane.PlaneBehaviour>();
            if (plane != null && plane.controller == Airplane.PlaneController.Player) {
                Airplane.PlaneBehaviour playerPlaneBehaviour = playerPlane.GetComponent<Airplane.PlaneBehaviour>();
                // 飞机停止动作
                PlaneMovementControllerService.GetInstance().SetPlane(playerPlaneBehaviour).StopPlane();
            
                // 镜头动画
                CameraService.GetInstance().SpotTrack(transParamII1.position, enemyBomberList[0].transform, 5f, () => {
                    PlaneMovementControllerService.GetInstance().SetPlane(playerPlaneBehaviour).RestorePlane();
                    DialogService.GetInstance().Hint("Take advantage of the victory and pursue the retreating enemy bombers.");
                    MissionService.GetInstance().CreateMissionPoint(firstMissionPoint, firstPointEnter, true, true);
                });
                Destroy(mission1Point.gameObject);
            }
        }

        private void firstPointEnter(Collider other) {
            Airplane.PlaneBehaviour plane = other.gameObject.GetComponentInParent<Airplane.PlaneBehaviour>();
            if (plane != null && plane.controller == Airplane.PlaneController.Player) {
                // 销毁轰炸机
                foreach (GameObject enemyBomber in enemyBomberList) {
                    Destroy(enemyBomber);
                }
                // 生成敌方战斗机
                GameObject enemyFighter1 = PlaneFactory.GetInstance().CreateEnemyFighter(transParamII2.position, transParamII2.rotation);
                GameObject enemyFighter2 = PlaneFactory.GetInstance().CreateEnemyFighter(transParamII3.position, transParamII3.rotation);
                GameObject enemyFighter3 = PlaneFactory.GetInstance().CreateEnemyFighter(transParamII4.position, transParamII4.rotation);
                enemyFighter1.GetComponent<PlaneWeaponBehaviour>().enabled = false;
                enemyFighter2.GetComponent<PlaneWeaponBehaviour>().enabled = false;
                enemyFighter3.GetComponent<PlaneWeaponBehaviour>().enabled = false;
                // 飞机停止动作
                Airplane.PlaneBehaviour playerPlaneBehaviour = playerPlane.GetComponent<Airplane.PlaneBehaviour>();
                PlaneMovementControllerService.GetInstance().SetPlane(playerPlaneBehaviour).StopPlane();
                CameraService.GetInstance().SpotTrack(transParamII5.position, transParamII2, 3f,
                    () => {
                        PlaneMovementControllerService.GetInstance().SetPlane(playerPlaneBehaviour)
                            .RestorePlane();
                        DialogService.GetInstance().Hint("Go back to our base along the point.");
                        enemyFighter1.GetComponent<PlaneWeaponBehaviour>().enabled = true;
                        enemyFighter2.GetComponent<PlaneWeaponBehaviour>().enabled = true;
                        enemyFighter3.GetComponent<PlaneWeaponBehaviour>().enabled = true;
                    });
                transParamII2.DOMove(transParamII3.position, 3f);
                // 更新任务进度，生成下一个任务点
                mission2.UpdateCurrentProgress();
                CreateNextMissionPoint();
                mission2.SetDescription("Go back to our base along the point.");
            }
        }

        private void CreateNextMissionPoint() {
            if (cursor > level2MissionPointList.Count - 1) {
                MissionService.GetInstance().Complete(mission2);
                return;
            }
            int currentCursor = cursor;
            MissionService.GetInstance().CreateMissionPoint(level2MissionPointList[currentCursor], other => {
                Airplane.PlaneBehaviour plane = other.gameObject.GetComponentInParent<Airplane.PlaneBehaviour>();
                if (plane != null && plane.controller == Airplane.PlaneController.Player) {
                    // 更新任务进度，生成下一个任务点
                    mission2.UpdateCurrentProgress();
                    cursor++;
                    CreateNextMissionPoint();
                }
            }, true, true);
        }

        public void FailMission1() {
            MissionService.GetInstance().Fail(mission1);
        }
    }
}