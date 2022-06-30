using System.Collections.Generic;
using Airplane.Bot.MovesetAction;

namespace Airplane.Bot.Moveset {
    public class TurnBack : Moveset {
        public TurnBack() {
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new PitchUp(180f));
            movesetActionQueue.Enqueue(new RollRight(180f));
        }
    }
}