using System;
using Plane.Health;
using Plane.Movement;
using Plane.Weapon;
using UnityEngine;

namespace Plane {
    public class PlaneBehaviour : MonoBehaviour {

        //------------------field ---------------------------------------
        
        /// <summary>
        /// 飞机配置
        /// </summary>
        public PlaneConfig planeConfig;

        /// <summary>
        /// 飞机阵营
        /// </summary>
        public Camp camp;

        /// <summary>
        /// 飞机运动
        /// </summary>
        private PlaneMovementBehaviour planeMovement;

        /// <summary>
        /// 飞机武器
        /// </summary>
        private PlaneWeaponBehaviour planeWeapon;

        /// <summary>
        /// 飞机生命
        /// </summary>
        private PlaneHealthBehaviour planeHealth;
        
        // ---------------------mono method -------------------------------
        
        private void Awake() {
            planeMovement = GetComponent<PlaneMovementBehaviour>();
            planeWeapon = GetComponent<PlaneWeaponBehaviour>();
            planeHealth = GetComponent<PlaneHealthBehaviour>();
        }
        
        //-------------------------getter & setter ----------------------
        
        public PlaneMovementBehaviour GetPlaneMovement() {
            return planeMovement;
        }
        
        public PlaneWeaponBehaviour GetPlaneWeapon() {
            return planeWeapon;
        }
        
        public PlaneHealthBehaviour GetPlaneHealth() {
            return planeHealth;
        }
    }
}