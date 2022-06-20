using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plane.Weapon {
    
    /// <summary>
    /// 负责飞机的武器系统
    /// </summary>
    public class PlaneWeaponBehaviour : MonoBehaviour {

        // -------------------------------field -----------------------------------------------
        
        /// <summary>
        /// 枪口位置
        /// </summary>
        public List<Transform> muzzleTransformList;

        /// <summary>
        /// 子弹预制体
        /// </summary>
        public GameObject bulletPrefab;

        /// <summary>
        /// 武器伤害
        /// </summary>
        private float damage;

        /// <summary>
        /// 开火间隔
        /// </summary>
        private float fireInterval;
        
        /// <summary>
        /// 子弹射程
        /// </summary>
        private float bulletRange;

        /// <summary>
        /// 子弹初速度
        /// </summary>
        private float bulletInitialSpeed;

        /// <summary>
        /// 子弹飞行速度
        /// </summary>
        private float bulletFlightSpeed;

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
            PlaneWeaponConfig planeWeaponConfig = GetComponent<PlaneBehaviour>().planeConfig.planeWeaponConfig;
            damage = planeWeaponConfig.damage;
            fireInterval = planeWeaponConfig.fireInterval;
            bulletRange = planeWeaponConfig.bulletRange;
            bulletInitialSpeed = planeWeaponConfig.bulletInitialSpeed;
            bulletFlightSpeed = planeWeaponConfig.bulletFlightSpeed;
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
        /// 子弹射程
        /// </summary>
        public float GetBulletRange() {
            return bulletRange;
        }

        /// <summary>
        /// 子弹初速度
        /// </summary>
        public float GetBulletInitialSpeed() {
            return bulletInitialSpeed;
        }

        /// <summary>
        /// 子弹飞行速度
        /// </summary>
        public float GetBulletFlightSpeed() {
            return bulletFlightSpeed;
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