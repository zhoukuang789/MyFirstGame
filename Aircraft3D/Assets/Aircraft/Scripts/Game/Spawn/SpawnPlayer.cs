using UnityEngine;
using com;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject prefab;

    public Transform parentTransform;
    public Transform spawnTrans;

    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        var plane = Instantiate(prefab, spawnTrans.position, spawnTrans.rotation, parentTransform);
        plane.SetActive(true);
        plane.name = "我";
        ReferenceService.instance.playerPlane = plane.GetComponent<PlaneBehaviour>();
        Debug.Log("player spawn!");
    }
}
