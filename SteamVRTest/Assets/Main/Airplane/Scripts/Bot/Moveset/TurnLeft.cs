using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    public class TurnLeft : Moveset{
        /// <summary>
        /// 左转angel度
        /// </summary>
        public TurnLeft(float angle, Vector3 positionBeforeAction) {
            name = MovesetName.TurnLeft;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new RollLeft(90f));
            movesetActionQueue.Enqueue(new PitchUp(angle));
            movesetActionQueue.Enqueue(new RollRight(90f));
            movesetActionQueue.Enqueue(new FlyForward(50f, positionBeforeAction));
        }
    }
}