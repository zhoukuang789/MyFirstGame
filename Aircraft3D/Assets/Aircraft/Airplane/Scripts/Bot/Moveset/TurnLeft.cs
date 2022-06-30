using System.Collections.Generic;
using Airplane.Bot.MovesetAction;

namespace Airplane.Bot.Moveset {
    public class TurnLeft : Moveset{
        public TurnLeft() {
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new RollLeft(90f));
            movesetActionQueue.Enqueue(new PitchUp(90f));
            movesetActionQueue.Enqueue(new RollRight(90f));
        }
    }
}