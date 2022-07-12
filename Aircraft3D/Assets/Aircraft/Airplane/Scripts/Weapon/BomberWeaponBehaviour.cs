using System.Collections.Generic;
using UnityEngine;

namespace Airplane.Weapon {
    
    
    public class BomberWeaponBehaviour : MonoBehaviour {
        // -------------------------------field -----------------------------------------------
        
        /// <summary>
        /// 投弹位置
        /// </summary>
        public List<Transform> muzzleTransformList;

        /// <summary>
        /// 炸弹预制体
        /// </summary>
        private GameObject bombPrefab;

        /// <summary>
        /// 炸弹伤害
        /// </summary>
        private float damage;

        /// <summary>
        /// 炸弹移动速度
        /// </summary>
        private float speed;

        /// <summary>
        /// 开火间隔
        /// </summary>
        private float fireInterval;

        /// <summary>
        /// 冷却时间
        /// </summary>
        private float cooldownTime;

        /// <summary>
        /// 上一次Update的时间
        /// </summary>
        private float lastTime;

        // ---------------------------mono method----------------------------------
        private void Awake() {
            BomberWeaponConfig bomberWeaponConfig = GetComponent<PlaneBehaviour>().planeConfig.bomberWeaponConfig;
            damage = bomberWeaponConfig.damage;
            fireInterval = bomberWeaponConfig.fireInterval;
            speed = bomberWeaponConfig.speed;

            bombPrefab = Resources.Load<GameObject>("Prefabs/Bomb");
        }
        
        private void Start() {
            cooldownTime = 0f;
            lastTime = Time.time;
        }

        private void Update() {
            cooldownTime -= (Time.time - lastTime);
            lastTime = Time.time;
        }

        // ---------------------------Getter & Setter----------------------------------
        public GameObject GetBombPrefab() {
            return bombPrefab;
        }
        
        /// <summary>
        /// 武器伤害
        /// </summary>
        public float GetDamage() {
            return damage;
        }

        /// <summary>
        /// 开火间隔
        /// </summary>
        public float GetFireInterval() {
            return fireInterval;
        }

        /// <summary>
        /// 炸弹飞行速度
        /// </summary>
        /// <returns></returns>
        public float GetSpeed() {
            return speed;
        }

        /// <summary>
        /// 冷却时间
        /// </summary>
        public float GetCooldownTime() {
            return cooldownTime;
        }

        /// <summary>
        /// 重置冷却时间
        /// </summary>
        public void ResetCooldownTime() {
            cooldownTime = fireInterval;
        }
    }
    
    
}