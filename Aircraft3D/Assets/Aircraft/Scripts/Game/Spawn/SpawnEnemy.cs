using UnityEngine;
using com;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform parentTransform;

    // Use this for initialization
    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        var playerPlane = ReferenceService.instance.playerPlane.transform;

        Spawn("敌人1", playerPlane.transform.position - playerPlane.transform.forward * 60 + playerPlane.transform.right * 30, playerPlane.transform.rotation, enemyPrefab);
        Spawn("敌人2", playerPlane.transform.position - playerPlane.transform.forward * 60 + playerPlane.transform.right * (-30), playerPlane.transform.rotation, enemyPrefab);

        Debug.Log("SpawnEnemies spawn!");
    }

    void Spawn(string name, Vector3 pos, Quaternion rot, GameObject enemyPrefab)
    {
        var plane = Instantiate(enemyPrefab, pos, rot, parentTransform);
        plane.SetActive(true);
        plane.name = name;
    }
}
