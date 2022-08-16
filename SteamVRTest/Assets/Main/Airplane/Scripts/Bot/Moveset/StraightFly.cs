using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    public class StraightFly : Moveset {

        public StraightFly(float distance, Vector3 currentPosition) {
            name = MovesetName.StraightFly;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new FlyForward(distance, currentPosition));
        }
        
    }
}