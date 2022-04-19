using UnityEngine;
using com;

public class SpawnService : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public Transform spawnPlayerTrans;

    void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
        Debug.Log("spawn done!");
    }

    void SpawnEnemies()
    {
        var playerPlane = ReferenceService.instance.playerPlane.transform;

        SpawnEnemy("敌人1", playerPlane.transform.position - playerPlane.transform.forward * 60 + playerPlane.transform.right * 30, playerPlane.transform.rotation, enemyPrefab);
        SpawnEnemy("敌人2", playerPlane.transform.position - playerPlane.transform.forward * 60 + playerPlane.transform.right * (-30), playerPlane.transform.rotation, enemyPrefab);
    }

    void SpawnEnemy(string name, Vector3 pos, Quaternion rot, GameObject enemyPrefab)
    {
        var plane = Instantiate(enemyPrefab, pos, rot, GetSpawnPlaneParent());
        plane.SetActive(true);
        plane.name = name;
    }

    void SpawnPlayer()
    {
        var plane = Instantiate(playerPrefab, spawnPlayerTrans.position, spawnPlayerTrans.rotation, GetSpawnPlaneParent());
        plane.SetActive(true);
        plane.name = "玩家";
        ReferenceService.instance.playerPlane = plane.GetComponent<PlaneBehaviour>();
    }

    Transform GetSpawnPlaneParent()
    {
        return ReferenceService.instance.planesParent;
    }
}
