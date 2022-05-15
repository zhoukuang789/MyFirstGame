using UnityEngine;
using com;
using System.Collections.Generic;

public class SpawnService : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBomberPrefab;
    public GameObject playerPrefab;

    public Transform spawnPlayerTrans;

    public List<Transform> enemyPos;
    public List<Transform> enemyBomberPos;

    public List<PlaneBehaviour> runtimeEnemies;

    public static SpawnService instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        runtimeEnemies = new List<PlaneBehaviour>();
        SpawnPlayer();
        //SpawnEnemies();
        //Debug.Log("spawn done!");
    }

    public void SpawnBombers()
    {
        int i = 0;
        foreach (var p in enemyBomberPos)
        {
            i++;
            SpawnEnemy("EnemyBomber_" + i, p.position, p.rotation, enemyBomberPrefab);
        }
    }

    void SpawnEnemies()
    {
        //var playerPlane = ReferenceService.instance.playerPlane.transform;
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
        runtimeEnemies.Add(plane.GetComponent<PlaneBehaviour>());
        Debug.Log("SpawnEnemy");
    }

    void SpawnPlayer()
    {
        var plane = Instantiate(playerPrefab, spawnPlayerTrans.position, spawnPlayerTrans.rotation, GetSpawnPlaneParent());
        plane.SetActive(true);
        plane.name = "PlayerPlane";
        ReferenceService.instance.playerPlane = plane.GetComponent<PlaneBehaviour>();

        FollowCam.instance.Init();
    }

    private Vector3 _rbVeloCache;

    Transform GetSpawnPlaneParent()
    {
        return ReferenceService.instance.planesParent;
    }

    public void PausePlayerPlane()
    {
        ReferenceService.instance.playerPlane.controller.enabled = false;
        _rbVeloCache = ReferenceService.instance.playerPlane.movement.rb.velocity;
        ReferenceService.instance.playerPlane.movement.rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void PausePlayerCam()
    {
        FollowCam.instance.enabled = false;
        CameraShake.instance.enabled = false;
    }

    public void ResumePlayerPlane()
    {
        ReferenceService.instance.playerPlane.controller.enabled = true;
        ReferenceService.instance.playerPlane.movement.rb.constraints = RigidbodyConstraints.None;
    }

    public void ResumePlayerCam()
    {
        FollowCam.instance.enabled = true;
        Camera.main.transform.localPosition = Vector3.zero;
        Camera.main.transform.localEulerAngles = Vector3.zero;
        CameraShake.instance.enabled = true;
        ReferenceService.instance.playerPlane.movement.rb.velocity = _rbVeloCache;
    }
}
