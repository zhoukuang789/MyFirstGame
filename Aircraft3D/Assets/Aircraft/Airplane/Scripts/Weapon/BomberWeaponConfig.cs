using UnityEngine;

namespace Airplane.Weapon {
    [CreateAssetMenu(fileName = "BomberWeaponConfig", menuName = "BomberWeaponConfig", order = 0)]
    public class BomberWeaponConfig : ScriptableObject {
        
        /// <summary>
        /// 武器伤害
        /// </summary>
        public float damage;

        /// <summary>
        /// 开火间隔
        /// </summary>
        public float fireInterval;

        /// <summary>
        /// 炸弹移动速度
        /// </summary>
        public float speed;
    }
}