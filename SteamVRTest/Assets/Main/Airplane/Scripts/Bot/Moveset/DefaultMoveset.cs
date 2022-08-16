using System.Collections.Generic;
using Airplane.Bot.MovesetAction;
using UnityEngine;

namespace Airplane.Bot.Moveset {
    public class DefaultMoveset : Moveset {
        /// <summary>
        /// 以angle度 爬升height米
        /// </summary>
        public DefaultMoveset() {
            name = MovesetName.DefaultMoveset;
            movesetActionQueue = new Queue<MovesetAction.MovesetAction>();
            movesetActionQueue.Enqueue(new RestorePosture());
            movesetActionQueue.Enqueue(new FlyForward(-1));
        }
        
    }
}