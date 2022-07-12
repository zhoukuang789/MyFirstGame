using Airplane.Bot;
using MyCamera;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace Airplane {
    public class PlaneFactory : Singletonable<PlaneFactory> {

        /// <summary>
        /// 创建玩家飞机
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parentTransform"></param>
        public void CreatePlayerPlane(Vector3 position, Quaternion rotation) {
            GameObject playerPlanePrefab = Resources.Load<GameObject>("Prefabs/Player");
            GameObject playerPlane = GameObject.Instantiate(playerPlanePrefab, position, rotation);
            playerPlane.name = "PlayerPlane";
            playerPlane.SetActive(true);

            Camera.main.GetComponent<CameraBehaviour>().target = playerPlane.transform;
        }

        public void CreateEnemyBomber(Vector3 position, Quaternion rotation) {
            GameObject enemyBomberPrefab = Resources.Load<GameObject>("Prefabs/EnemyBomber");
            GameObject enemyBomber = GameObject.Instantiate(enemyBomberPrefab, position, rotation);
            enemyBomber.name = "EnemyBomber";
            enemyBomber.SetActive(true);
        }
        
    }
}