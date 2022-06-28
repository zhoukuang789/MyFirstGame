using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Airplane.Bot {
    
    public class Climb : Moveset {

        public Climb() {
            movesetActionQueue = new Queue<MovesetAction>();
            // movesetActionQueue.Enqueue(MovesetAction.RestorePosture);
            // movesetActionQueue.Enqueue(MovesetAction.PitchUp);
            // movesetActionQueue.Enqueue(MovesetAction.DirectFly);
            // movesetActionQueue.Enqueue(MovesetAction.RestorePosture);
        }

    }
}