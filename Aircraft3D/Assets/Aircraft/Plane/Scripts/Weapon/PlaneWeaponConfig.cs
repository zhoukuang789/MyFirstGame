using UnityEngine;

namespace Plane.Weapon {
    [CreateAssetMenu(fileName = "PlaneWeaponConfig", menuName = "PlaneWeaponConfig", order = 0)]
    public class PlaneWeaponConfig : ScriptableObject {
        
        /// <summary>
        /// 武器伤害
        /// </summary>
        public float damage;

        /// <summary>
        /// 开火间隔
        /// </summary>
        public float fireInterval;
        
        /// <summary>
        /// 子弹射程
        /// </summary>
        public float bulletRange;

        /// <summary>
        /// 子弹初速度
        /// </summary>
        public float bulletInitialSpeed;

        /// <summary>
        /// 子弹飞行速度
        /// </summary>
        public float bulletFlightSpeed;
    }
}