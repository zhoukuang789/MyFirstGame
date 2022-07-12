﻿using System;
using System.Collections.Generic;
using Airplane;
using Mission;
using UnityEngine;
using System.Collections;
using Airplane.Movement;
using com;
using DG.Tweening;
using Dialog.Scripts;
using MyCamera;
using UnityEngine.SceneManagement;

namespace GameManager {
    public enum LevelNumber {
        Level1,
        Level2,
        Level3
    }

    public class GameManager : MonoBehaviour {
        public LevelNumber levelNumber;

        public CameraBehaviour mainCamera;

        [Header("Level 1")]
        public Transform playerPlaneSpawnTransformInLevel1;

        public Transform enemyBomberSpawnTransformInLevel1;
        public Transform missionPointTransform;
        public Transform transParam1;
        public Transform transParam2;
        public Transform transParam3;
        public Transform transParam4;
        private Mission.MissionItem mission1;
        private int currentKillBomberNumber;
        private MissionPointBehaviour mission1Point;

        [Header("Level 2")]
        public Transform playerPlaneSpawnTransformInLevel2;

        private void Start() {
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
            
            // 生成玩家飞机
            PlaneFactory.GetInstance().CreatePlayerPlane(playerPlaneSpawnTransformInLevel1.position,
                playerPlaneSpawnTransformInLevel1.rotation);
            
            // 生成敌军飞机
            PlaneFactory.GetInstance().CreateEnemyBomber(enemyBomberSpawnTransformInLevel1.position,
                enemyBomberSpawnTransformInLevel1.rotation);
            PlaneFactory.GetInstance()
                .CreateEnemyBomber(enemyBomberSpawnTransformInLevel1.position + new Vector3(50f, 0, 0f),
                    enemyBomberSpawnTransformInLevel1.rotation);

            // 创建关卡1任务
            mission1 = MissionService.GetInstance().CreateMission()
                .SetName("任务1")
                .SetDescription("摧毁两架敌军轰炸机。")
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
                    // 显示完成菜单
                    MenuBehaviour completeMenu = DialogService.GetInstance().ShowMenu("mission complete");
                    completeMenu.AddButton("Next", () => {
                        SceneManager.LoadScene(2);
                        Destroy(completeMenu.gameObject);
                    });
                    completeMenu.AddButton("Quit", () => {
                        SceneManager.LoadScene(0);
                        Destroy(completeMenu.gameObject);
                    });
                })
                .SetOnFail(() => {
                    // 任务失败时执行
                    // 显示失败菜单
                    MenuBehaviour failMenu = DialogService.GetInstance().ShowMenu("mission failed");
                    failMenu.AddButton("Retry", () => {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        Destroy(failMenu.gameObject);
                    });
                    failMenu.AddButton("Quit", () => {
                        SceneManager.LoadScene(0);
                        Destroy(failMenu.gameObject);
                    });
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
                Airplane.PlaneBehaviour playerPlane =
                    GameObject.Find("PlayerPlane").GetComponent<Airplane.PlaneBehaviour>();
                // 飞机停止动作
                PlaneMovementControllerService.GetInstance().SetPlane(playerPlane).StopPlane();

                // 镜头动画
                mainCamera.ChangeTrackingMode(CameraTrackingMode.Spot, 3f, transParam3, transParam2.position);
                transParam3.DOMove(transParam4.position, 3f);
                
                Timer timer = TimerManager.instance.GetTimer();
                timer.Init(null, null, () => {
                    GameObject enemyBomber = GameObject.Find("EnemyBomber");
                    mainCamera.ChangeTrackingMode(CameraTrackingMode.Spot, 5f, enemyBomber.transform,
                        transParam1.position,
                        () => {
                            PlaneMovementControllerService.GetInstance().SetPlane(playerPlane).RestorePlane();
                            DialogService.GetInstance().Hint("保护我方设施，并消灭敌军至少2驾轰炸机。");
                        });
                }, "test", 1, 2);
                timer.Start();

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
            PlaneFactory.GetInstance().CreatePlayerPlane(playerPlaneSpawnTransformInLevel2.position,
                playerPlaneSpawnTransformInLevel2.rotation);
        }

        public void FailMission1() {
            MissionService.GetInstance().Fail(mission1);
        }
    }
}