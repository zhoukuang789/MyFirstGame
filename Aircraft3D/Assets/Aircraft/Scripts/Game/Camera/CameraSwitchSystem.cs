using UnityEngine;
using MyCamera;

public class CameraSwitchSystem : MonoBehaviour
{
    public static CameraSwitchSystem instance;

    public KeyCode triggerKey = KeyCode.V;
    public bool setOnPrepared;
    public bool preparedSetFirstOrThirdPerson;

    Camera _fpCam;
    Camera _tpCam;
    bool _prepared;

    private void Awake()
    {
        instance = this;
        _prepared = false;
    }

    private void Start()
    {
        AssignCams();
    }

    void AssignCams()
    {
        _fpCam = CameraBehaviour.fpInstance?.GetComponent<Camera>();
        _tpCam = CameraBehaviour.tpInstance?.GetComponent<Camera>();
        if (_fpCam != null && _tpCam != null)
        {
            _prepared = true;
            Debug.Log("CameraSwitchSystem Prepared");
            if (setOnPrepared)
            {
                if (preparedSetFirstOrThirdPerson)
                    SetFirstPerson();
                else
                    SetThirdPerson();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            ToggleView();
        }
    }

    void ToggleView()
    {
        if (!_prepared)
        {
            AssignCams();
            if (!_prepared)
                return;
        }

        if (_tpCam.enabled)
            SetFirstPerson();
        else
            SetThirdPerson();
    }

    public void SetFirstPerson()
    {
        if (!_prepared)
        {
            AssignCams();
            if (!_prepared)
                return;
        }

        _fpCam.enabled = true;
        _tpCam.enabled = false;
    }

    public void SetThirdPerson()
    {
        if (!_prepared)
        {
            AssignCams();
            if (!_prepared)
                return;
        }

        _fpCam.enabled = false;
        _tpCam.enabled = true;
    }
}
