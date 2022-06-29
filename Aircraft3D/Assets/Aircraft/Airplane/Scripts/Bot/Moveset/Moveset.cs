using System.Collections.Generic;


namespace Airplane.Bot.Moveset {
    
    public class Moveset {
        
        /// <summary>
        /// 需要完成的movesetAction
        /// </summary>
        protected Queue<MovesetAction.MovesetAction> movesetActionQueue;
        
        public Queue<MovesetAction.MovesetAction> GetMovesetActionQueue() {
            return movesetActionQueue;
        }
        

    }
}