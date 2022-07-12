using ProjectBase.SingletonBase;
using UnityEngine;

namespace Airplane.Movement {
    public class PlaneMovementControllerService : Singletonable<PlaneMovementControllerService> {
        
        //------------------field ---------------------------------------
        private PlaneBehaviour plane;

        private PlaneMovementBehaviour planeMovement;

        
        //-------------------------getter & setter ----------------------
        public PlaneMovementControllerService SetPlane(PlaneBehaviour plane) {
            this.plane = plane;
            planeMovement = plane.GetPlaneMovement();
            return this;
        }

        
        // ---------------------function -------------------------------
        
        /// <summary>
        /// 增加推力
        /// </summary>
        /// <param name="volume">表示操作杆杆量，操作杆推到底表示1，操作杆处于原地表示0，键盘操控时，可以根据按键时长表示杆量，比如按住w2秒以上表示杆量拉满</param>
        public void AddTrust(float volume) {
            if (planeMovement.GetCurrentSpeedOnForward() >=
                planeMovement.GetMaxSpeedOnForward()) {
                return;
            }
            planeMovement.GetRigidbody().AddForce(planeMovement.transform.forward * planeMovement.GetAccelerateTrustSize(volume));
        }

        /// <summary>
        /// 减小推力，同上
        /// </summary>
        /// <param name="volume">操作杆杆量</param>
        public void ReduceTrust(float volume) {
            if (planeMovement.GetCurrentSpeedOnForward() <= 5f) {
                return;
            }
            planeMovement.GetRigidbody().AddForce(-planeMovement.transform.forward * planeMovement.GetDecelerateTrustSize(volume));
        }
        
        /// <summary>
        /// 俯仰操作
        /// </summary>
        /// <param name="volume">杆量 -1~1 </param>
        public float DoPitch(float volume) {
            float angle = planeMovement.GetPitchRate(volume);
            planeMovement.Rotate(angle, -planeMovement.transform.right);
            return angle;
        }

        /// <summary>
        /// 偏航操作
        /// </summary>
        /// <param name="volume">杆量 -1~1 </param>
        public void DoYaw(float volume) {
            float angle = planeMovement.GetYawRate(volume);
            planeMovement.Rotate(angle, -planeMovement.transform.up);
        }

        /// <summary>
        /// 滚转操作
        /// </summary>
        /// <param name="volume">-1~1</param>
        public float DoRoll(float volume) {
            float angle = planeMovement.GetRollRate(volume);
            planeMovement.Rotate(angle, -planeMovement.transform.forward);
            return angle;
        }

        public float GetSpeed() {
            return plane.GetPlaneMovement().GetRigidbody().velocity.magnitude;
        }

        public void StopPlane() {
            plane.GetComponent<Rigidbody>().useGravity = false;
            plane.GetComponent<PlaneMovementBehaviour>().enabled = false;
        }

        public void RestorePlane() {
            plane.GetComponent<Rigidbody>().useGravity = true;
            plane.GetComponent<PlaneMovementBehaviour>().enabled = true;
        }
    }
}