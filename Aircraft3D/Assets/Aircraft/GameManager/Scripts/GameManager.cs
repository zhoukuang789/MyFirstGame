using System;
using System.Collections.Generic;
using Airplane;
using Mission;
using UnityEngine;
using System.Collections;

namespace GameManager
{

    public enum LevelNumber
    {
        Level1,
        Level2,
        Level3
    }

    public class GameManager : MonoBehaviour
    {

        public LevelNumber levelNumber;

        [Header("Level 1")]
        public Transform playerPlaneSpawnTransformInLevel1;
        public Transform enemyBomberSpawnTransformInLevel1;
        private Mission.MissionItem mission1;
        private int currentKillBomberNumber;

        private void Start()
        {
            StartCoroutine("StartMissions");
        }

        IEnumerator StartMissions()
        {
            yield return new WaitForSeconds(0.1f);
            switch (levelNumber)
            {
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

        private void Level1Init()
        {
            PlaneFactory.GetInstance().CreatePlayerPlane(playerPlaneSpawnTransformInLevel1.position, playerPlaneSpawnTransformInLevel1.rotation);
            PlaneFactory.GetInstance().CreateEnemyBomber(enemyBomberSpawnTransformInLevel1.position, enemyBomberSpawnTransformInLevel1.rotation);
            PlaneFactory.GetInstance().CreateEnemyBomber(enemyBomberSpawnTransformInLevel1.position + new Vector3(0f, 50f, 0f), enemyBomberSpawnTransformInLevel1.rotation);

            mission1 = MissionService.GetInstance().CreateMission()
                .SetName("任务1")
                .SetDescription("摧毁两架敌军轰炸机。")
                .SetTotalProgress(2)
                .SetOnStart(() =>
                {
                    // 记下当前击杀轰炸机的数量
                    currentKillBomberNumber = Record.RecordService.GetInstance().GetKillRecord(PlaneType.Bomber);
                    Record.RecordService.GetInstance().AddKillRecordChangeEventListener(KillRecordChanged);
                })
                .SetOnComplete(() =>
                {
                    Debug.Log("任务1完成");
                })
                .SetOnFail(() =>
                {
                    Debug.Log("任务1失败");
                });
            MissionService.GetInstance().Start(mission1);
        }

        private void KillRecordChanged(Dictionary<PlaneType, int> map)
        {
            if (map[PlaneType.Bomber] - currentKillBomberNumber > 0)
            {
                mission1.UpdateCurrentProgress();
                if (mission1.GetCurrentProgress() == mission1.GetTotalProgress())
                {
                    MissionService.GetInstance().Complete(mission1);
                }
            }
        }

        private void Level2Init()
        {

        }
    }


}