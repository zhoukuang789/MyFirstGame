using UnityEngine;

namespace Airplane.Bot {
    
    public class MovesetAction {

        /// <summary>
        /// 完成目标的值
        /// </summary>
        protected float value;

        /// <summary>
        /// 该行为是否完成
        /// </summary>
        protected bool isComplete;

        public virtual void DoAction(PlaneBehaviour plane, float pitchAngle, float rollAngle) {
            
        }

    }
}