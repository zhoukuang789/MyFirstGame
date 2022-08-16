using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    
    public class Climb : Moveset {
        /// <summary>
        /// 以angle度 爬升height米
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="height"></param>
        /// <param name="positionBeforeAction"></param>
        public Climb(float angle, float height, Vector3 positionBeforeAction) {
            name = MovesetName.Climb;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new PitchUp(angle));
            movesetActionQueue.Enqueue(new FlyForward(height / Mathf.Sin(angle * Mathf.Deg2Rad), positionBeforeAction));
            movesetActionQueue.Enqueue(new RestorePosture());
        }

    }
}