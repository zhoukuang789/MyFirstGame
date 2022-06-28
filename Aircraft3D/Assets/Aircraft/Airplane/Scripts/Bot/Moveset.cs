using System.Collections.Generic;

namespace Airplane.Bot {
    
    public class Moveset {
        
        /// <summary>
        /// 需要完成的movesetAction
        /// </summary>
        protected Queue<MovesetAction> movesetActionQueue;

        public Queue<MovesetAction> GetMovesetActionQueue() {
            return movesetActionQueue;
        }

    }
}