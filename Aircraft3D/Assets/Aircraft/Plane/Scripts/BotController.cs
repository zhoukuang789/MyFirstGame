using System;
using UnityEngine;
using UnityEngine.UI;

namespace Plane {
    public class BotController : MonoBehaviour {

        // -------------------field ----------------------------
        private PlaneBehaviour plane;

        // -------------------mono mathod ----------------------
        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
        }
        
        // ---------------------function -----------------------

        /// <summary>
        /// 恢复飞行姿态
        /// </summary>
        public void RestorePosture() {
            
        }
        
        /// <summary>
        /// 往前直飞
        /// </summary>
        public void FlyForward() {
            
        }

        /// <summary>
        /// 向上俯仰
        /// </summary>
        public void PitchUp() {
            
        }

        /// <summary>
        /// 向下俯仰
        /// </summary>
        public void PitchDown() {
            
        }

        /// <summary>
        /// 左转弯
        /// </summary>
        public void TurnLeft() {
            
        }

        /// <summary>
        /// 右转弯
        /// </summary>
        public void TurnRight() {
            
        }
    }
}