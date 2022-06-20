using ProjectBase.SingletonBase;

namespace Plane.Movement {
    public class PlaneMovementControllerService : Singletonable<PlaneMovementControllerService> {
        
        private PlaneBehaviour plane;

        public PlaneMovementControllerService SetPlane(PlaneBehaviour plane) {
            this.plane = plane;
            return this;
        }

        /// <summary>
        /// 增加推力
        /// </summary>
        /// <param name="volume">表示操作杆杆量，操作杆推到底表示1，操作杆处于原地表示0，键盘操控时，可以根据按键时长表示杆量，比如按住w2秒以上表示杆量拉满</param>
        public void AddTrust(float volume) {
            if (plane.GetPlaneMovement().GetCurrentSpeedOnForward() >=
                plane.GetPlaneMovement().GetMaxSpeedOnForward()) {
                return;
            }
            plane.GetRigidbody().AddForce(plane.transform.forward * plane.GetPlaneMovement().GetAccelerateTrustSize(volume));
        }

        /// <summary>
        /// 减小推力，同上
        /// </summary>
        /// <param name="volume">操作杆杆量</param>
        public void ReduceTrust(float volume) {
            if (plane.GetPlaneMovement().GetCurrentSpeedOnForward() <= 5f) {
                return;
            }
            plane.GetRigidbody().AddForce(-plane.transform.forward * plane.GetPlaneMovement().GetDecelerateTrustSize(volume));
        }
        
        /// <summary>
        /// 俯仰操作
        /// </summary>
        /// <param name="volume">杆量 -1~1 </param>
        public void DoPitch(float volume) {
            float angle = plane.GetPlaneMovement().GetPitchRate(volume);
            plane.GetPlaneMovement().Rotate(angle, -plane.transform.right);
        }

        /// <summary>
        /// 偏航操作
        /// </summary>
        /// <param name="volume">杆量 -1~1 </param>
        public void DoYaw(float volume) {
            float angle = plane.GetPlaneMovement().GetYawRate(volume);
            plane.GetPlaneMovement().Rotate(angle, -plane.transform.up);
        }

        /// <summary>
        /// 滚转操作
        /// </summary>
        /// <param name="volume">-1~1</param>
        public void DoRoll(float volume) {
            float angle = plane.GetPlaneMovement().GetRollRate(volume);
            plane.GetPlaneMovement().Rotate(angle, -plane.transform.forward);
        }
    }
}