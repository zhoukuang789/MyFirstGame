using System;
using ProjectBase.SingletonBase;

namespace Airplane.Health {
    public class PlaneHealthService : Singletonable<PlaneHealthService> {

        //------------------field ---------------------------------------
        private PlaneBehaviour plane;

        private PlaneHealthBehaviour planeHealth;

        private event Action<PlaneBehaviour> planeDeathEvent; 

        //-------------------------getter & setter ----------------------
        public PlaneHealthService SetPlane(PlaneBehaviour plane) {
            this.plane = plane;
            planeHealth = plane.GetPlaneHealth();
            return this;
        }
        
        /// <summary>
        /// 广播飞机死亡事件
        /// </summary>
        /// <param name="plane"></param>
        public void PublishPlaneDeathEvent() {
            if (planeDeathEvent != null)
                planeDeathEvent(plane);
        }

        /// <summary>
        /// 订阅飞机死亡事件
        /// </summary>
        /// <param name="action"></param>
        public void AddPlaneDeathEventListener(Action<PlaneBehaviour> action) {
            planeDeathEvent += action;
        }
        
        /// <summary>
        /// 取消订阅飞机死亡事件
        /// </summary>
        /// <param name="action"></param>
        public void RemovePlaneDeathEventListener(Action<PlaneBehaviour> action) {
            planeDeathEvent -= action;
        }

        // ---------------------function -------------------------------

        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="damage"></param>
        public void ReceiveDamage(float damage) {
            planeHealth.ReduceHealth(damage);
        }

    }

}