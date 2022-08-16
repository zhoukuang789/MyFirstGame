using UnityEngine;

namespace Airplane.Bot.MovesetAction {
    
    public class MovesetAction {
        
        // ----------------------field ----------------------------------

        /// <summary>
        /// 当前飞行姿态
        /// </summary>
        private readonly PlanePosture currentPlanePosture;
        
        /// <summary>
        /// 完成目标的值
        /// </summary>
        protected float value;

        /// <summary>
        /// 该动作是否完成
        /// </summary>
        protected bool isComplete;

        // ---------------------------getter & setter ------------------------------
        /// <summary>
        /// 此构造方法不会初始化planePostureBeforeAction
        /// </summary>
        protected MovesetAction() {
            currentPlanePosture = new PlanePosture();
        }

        /// <summary>
        /// 获飞机当前飞行姿态
        /// </summary>
        /// <param name="plane"></param>
        /// <returns></returns>
        protected PlanePosture GetCurrentPlanePosture(PlaneBehaviour plane) {
            currentPlanePosture.SetTransform(plane.transform);
            return currentPlanePosture;
        }

        public bool GetIsComplete() {
            return isComplete;
        }
        
        /// <summary>
        /// 手动达标
        /// </summary>
        public void Complete() {
            isComplete = true;
        }


        // ----------------------------function ------------------------------------
        public virtual void DoAction(PlaneBehaviour plane) {
            
        }

    }
}