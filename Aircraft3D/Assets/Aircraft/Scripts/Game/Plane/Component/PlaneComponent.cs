using UnityEngine;
using System.Collections;

public class PlaneComponent : MonoBehaviour
{
    [HideInInspector]
    public PlaneBehaviour plane;

    private bool _postStarted;

    private void Update()
    {
        if (!_postStarted)
        {
            _postStarted = true;
            PostStart();
        }

        OnUpdate();
    }

    protected virtual void PostStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }
}
