using Airplane.Bot;
// using MyCamera;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace Airplane
{
    public class PlaneFactory : Singletonable<PlaneFactory> {

        private PlaneBehaviour playerPlane;
        
        /// <summary>
        /// 创建玩家飞机
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parentTransform"></param>
        public GameObject CreatePlayerPlane(Vector3 position, Quaternion rotation)
        {
            GameObject playerPlanePrefab = Resources.Load<GameObject>("Prefabs/Player");
            GameObject playerPlane = GameObject.Instantiate(playerPlanePrefab, position, rotation);
            playerPlane.name = "PlayerPlane";
            playerPlane.SetActive(true);

            // var cam = Camera.main.GetComponent<CameraBehaviour>();
            // cam.target = playerPlane.transform;
            // cam.playerPlane = playerPlane.transform;

            this.playerPlane = playerPlane.GetComponent<PlaneBehaviour>();
            return playerPlane;
        }

        public PlaneBehaviour GetPlayerPlane() {
            return playerPlane;
        }

        public GameObject CreateEnemyBomber(Vector3 position, Quaternion rotation)
        {
            GameObject enemyBomberPrefab = Resources.Load<GameObject>("Prefabs/EnemyBomber");
            GameObject enemyBomber = GameObject.Instantiate(enemyBomberPrefab, position, rotation);
            enemyBomber.name = "EnemyBomber";
            enemyBomber.SetActive(true);
            return enemyBomber;
        }
        
        public GameObject CreateBackEnemyBomber(Vector3 position, Quaternion rotation)
        {
            GameObject enemyBomberPrefab = Resources.Load<GameObject>("Prefabs/BackEnemyBomber");
            GameObject enemyBomber = GameObject.Instantiate(enemyBomberPrefab, position, rotation);
            enemyBomber.name = "BackEnemyBomber";
            enemyBomber.SetActive(true);
            return enemyBomber;
        }
        
        public GameObject CreateEnemyFighter(Vector3 position, Quaternion rotation)
        {
            GameObject enemyBomberPrefab = Resources.Load<GameObject>("Prefabs/EnemyFighter");
            GameObject enemyBomber = GameObject.Instantiate(enemyBomberPrefab, position, rotation);
            enemyBomber.name = "EnemyFighter";
            enemyBomber.SetActive(true);
            return enemyBomber;
        }

    }
}