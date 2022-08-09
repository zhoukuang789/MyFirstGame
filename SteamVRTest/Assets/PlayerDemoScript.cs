using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerDemoScript : MonoBehaviour
{
    
    public bool showControllers;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (showControllers)
            {
                hand.ShowController();
                hand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithController);
            }
            else
            {
                hand.HideController();
                hand.SetSkeletonRangeOfMotion(EVRSkeletalMotionRange.WithoutController);
            }
        }
    }
}
