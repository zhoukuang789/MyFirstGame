using System.Collections.Generic;


namespace Airplane.Bot.Moveset {
    
    public class Moveset {
        
        protected MovesetName name;
        
        /// <summary>
        /// 需要完成的movesetAction
        /// </summary>
        protected Queue<MovesetAction.MovesetAction> movesetActionQueue;
        
        public Queue<MovesetAction.MovesetAction> GetMovesetActionQueue() {
            return movesetActionQueue;
        }

        public MovesetName GetName() {
            return name;
        }
        
        public bool Equals(Moveset moveset) {
            return name.Equals(moveset.name);
        }
    }
}