using ProjectBase.SingletonBase;

namespace Plane.Health {
    public class PlaneHealthService : Singletonable<PlaneBehaviour> {

        //------------------field ---------------------------------------
        private PlaneBehaviour plane;

        private PlaneHealthBehaviour planeHealth;

        //-------------------------getter & setter ----------------------
        public PlaneHealthService SetPlane(PlaneBehaviour plane) {
            this.plane = plane;
            planeHealth = plane.GetPlaneHealth();
            return this;
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