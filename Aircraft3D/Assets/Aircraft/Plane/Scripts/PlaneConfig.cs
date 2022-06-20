using System;
using UnityEngine;

namespace Plane {
    [CreateAssetMenu(fileName = "PlaneConfig", menuName = "PlaneConfig", order = 0)]
    public class PlaneConfig : ScriptableObject {

        /// <summary>
        /// 飞机型号
        /// </summary>
        public string model;

        /// <summary>
        /// 飞机介绍
        /// </summary>
        public string describe;
        
        /// <summary>
        /// 飞机运动配置
        /// </summary>
        public Movement.PlaneMovementConfig planeMovementConfig;

        /// <summary>
        /// 飞机武器配置
        /// </summary>
        public Weapon.PlaneWeaponConfig planeWeaponConfig;
    }
}