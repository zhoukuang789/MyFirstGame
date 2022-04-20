using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;

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
    private void FixedUpdate()
    {
        
        OnFixedUpdate();
    }

    protected virtual void PostStart()
    {

    }

    protected virtual void OnUpdate()
    {

    }
    
    protected virtual void OnFixedUpdate()
    {

    }
}
