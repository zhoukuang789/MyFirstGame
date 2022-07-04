using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    public class TurnBack : Moveset {
        public TurnBack() {
            name = MovesetName.TurnBack;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new PitchUp(180f));
            movesetActionQueue.Enqueue(new RollRight(180f));
        }
    }
}