using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    public class TurnRight : Moveset {
        /// <summary>
        /// 右转angle度
        /// </summary>
        public TurnRight(float angle, Vector3 positionBeforeAction) {
            name = MovesetName.TurnRight;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new RollRight(90f));
            movesetActionQueue.Enqueue(new PitchUp(angle));
            movesetActionQueue.Enqueue(new RollLeft(90f));
            movesetActionQueue.Enqueue(new FlyForward(50f, positionBeforeAction));
        }
    }
}