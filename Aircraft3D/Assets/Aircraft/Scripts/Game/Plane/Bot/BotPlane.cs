using UnityEngine;
using System.Collections;

public class BotPlane : PlaneBehaviour
{

    public BotPlaneController botController
    {
        get
        {
            return this.controller as BotPlaneController;
        }
    }
}
