using UnityEngine;
using System.Collections;

public class BotPlane : PlaneBehaviour
{
    public BotAi ai
    {
        get { return GetComponent<BotAi>(); }
    }

    public BotPlaneController botController
    {
        get
        {
            return this.controller as BotPlaneController;
        }
    }
}
