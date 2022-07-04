using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    public class Dive : Moveset {
        
        /// <summary>
        /// 以angle度 下降height米
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="height"></param>
        /// <param name="positionBeforeAction"></param>
        public Dive(float angle, float height, Vector3 positionBeforeAction) {
            name = MovesetName.Dive;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new PitchDown(angle));
            movesetActionQueue.Enqueue(new FlyForward(height / Mathf.Sin(angle * Mathf.Deg2Rad), positionBeforeAction));
            movesetActionQueue.Enqueue(new RestorePosture());
        }
    }
}