using UnityEngine;
using com;

public class MissileSystem : MonoBehaviour
{
    public float rechargeTime = 5;

    float _rechargeTimer;
    public Transform missile1Launcher;
    public Transform missile2Launcher;
    public Transform missileSpace;
    public MissileMove missilePrefab;
    public Transform target;

    private void Start()
    {
        _rechargeTimer = 0;
    }

    void Update()
    {
        if (GetMissileInput())
        {
            TryLaunch();
        }

        if (_rechargeTimer > 0)
        {
            _rechargeTimer -= GameTime.deltaTime;
        }
    }

    void TryLaunch()
    {
        if (_rechargeTimer > 0)
        {
            return;
        }

        Debug.Log("launch");
        _rechargeTimer = rechargeTime;
        var ms1 = Instantiate(missilePrefab, missile1Launcher.position, missile1Launcher.rotation, missileSpace);
        var ms2 = Instantiate(missilePrefab, missile2Launcher.position, missile2Launcher.rotation, missileSpace);
        ms1.Init(missile1Launcher.right, target);
        ms2.Init(missile2Launcher.right, target);
    }

    bool GetMissileInput()
    {
        //TODO adapt VR
        if (Input.GetKeyDown(KeyCode.M))
        {
            return true;
        }

        return false;
    }
}
