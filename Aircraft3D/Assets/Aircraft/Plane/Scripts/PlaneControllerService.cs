using ProjectBase.SingletonBase;
using UnityEngine;

namespace Plane {
    /// <summary>
    /// 这个类用于对外提供操控飞机的接口，包括加速、减速、俯仰、滚转、偏转、开火等等操作
    /// </summary>
    public class PlaneControllerService : Singletonable<PlaneControllerService> {
        private PlaneMoveBehaviour plane;
        public PlaneMoveBehaviour GetPlane() { return plane; }

        public PlaneControllerService SetPlane(PlaneMoveBehaviour plane) {
            this.plane = plane;
            return this;
        }

        /// <summary>
        /// 增加推力
        /// </summary>
        /// <param name="volume">表示操作杆杆量，操作杆推到底表示1，操作杆处于原地表示0，键盘操控时，可以根据按键时长表示杆量，比如按住w2秒以上表示杆量拉满</param>
        public void AddTrust(float volume) {
            if (plane.GetCurrentSpeed() >= plane.GetMaxSpeed()) return;
            plane.GetRigidbody().AddForce(plane.transform.forward * plane.GetAccelerateTrustSize(volume));
        }

        /// <summary>
        /// 减小推力，同上
        /// </summary>
        /// <param name="volume">操作杆杆量</param>
        public void ReduceTrust(float volume) {
            if (plane.GetCurrentSpeed() <= 5f) return;
            plane.GetRigidbody().AddForce(-plane.transform.forward * plane.GetDecelerateTrustSize(volume));
        }
        
        /// <summary>
        /// 俯仰操作
        /// </summary>
        /// <param name="volume">杆量 -1~1 </param>
        public void DoPitch(float volume) {
            float angle = plane.GetPitchRate(volume);
            plane.Rotate(angle, -plane.transform.right);
        }

        /// <summary>
        /// 向左偏航
        /// </summary>
        /// <param name="volume"></param>
        public void YawLeft(float volume) {
            float angle = plane.GetYawRate(volume);
            plane.Rotate(angle, -plane.transform.up);
        }

        /// <summary>
        /// 向右偏航
        /// </summary>
        /// <param name="volume"></param>
        public void YawRight(float volume) {
            float angle = plane.GetYawRate(volume);
            plane.Rotate(angle, plane.transform.up);
        }

        /// <summary>
        /// 滚转
        /// </summary>
        /// <param name="volume">-1~1</param>
        public void DoRoll(float volume) {
            float angle = plane.GetRollRate(volume);
            plane.Rotate(angle, -plane.transform.forward);
        }

    }
}