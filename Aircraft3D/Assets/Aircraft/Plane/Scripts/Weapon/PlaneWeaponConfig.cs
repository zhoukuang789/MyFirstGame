using UnityEngine;

namespace Plane.Weapon {
    [CreateAssetMenu(fileName = "PlaneWeaponConfig", menuName = "PlaneWeaponConfig", order = 0)]
    public class PlaneWeaponConfig : ScriptableObject {
        
        // 武器伤害
        public float damage;
    }
}