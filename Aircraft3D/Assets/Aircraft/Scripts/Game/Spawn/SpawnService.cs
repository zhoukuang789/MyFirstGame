using UnityEngine;
using com;
using System.Collections.Generic;

public class SpawnService : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public Transform spawnPlayerTrans;

    public List<Transform> enemyPos;

    void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
        Debug.Log("spawn done!");
    }

    void SpawnEnemies()
    {
        var playerPlane = ReferenceService.instance.playerPlane.transform;

        int i = 0;
        foreach (var p in enemyPos)
        {
            i++;
            SpawnEnemy("Enemy_" + i, p.position, p.rotation, enemyPrefab);
        }

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
        plane.name = "PlayerPlane";
        ReferenceService.instance.playerPlane = plane.GetComponent<PlaneBehaviour>();

        FollowCam.instance.Init();
    }

    Transform GetSpawnPlaneParent()
    {
        return ReferenceService.instance.planesParent;
    }
}
