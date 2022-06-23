using System;
using Record;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Plane.Health {
    public class PlaneHealthBehaviour : MonoBehaviour {
        
        // --------------------field -------------------------------
        
        /// <summary>
        /// 飞机生命值
        /// </summary>
        private float health = 100f;
        
        /// <summary>
        /// 子弹击中特效
        /// </summary>
        public GameObject hitEffect;
        
        /// <summary>
        /// 飞机死亡特效
        /// </summary>
        public GameObject deathEffect;
        
        /// <summary>
        /// 冒烟特效
        /// </summary>
        public GameObject smokeEffect;
        
        /// <summary>
        /// 飞机冒烟位置
        /// </summary>
        public Transform smokeTransform;

        // -----------------getter & setter -------------------------
        
        /// <summary>
        /// 减少生命值，如果生命值低于30%，飞机冒烟；生命值低于0，飞机死亡
        /// </summary>
        /// <param name="damage"></param>
        public void ReduceHealth(float damage) {
            health -= damage;
            if (health <= health * 0.3f) {
                Smoke();
            }
            if (health <= 0f) {
                PlaneDie();
            }
        }

        /// <summary>
        /// 飞机冒烟
        /// </summary>
        private void Smoke() {
            // TODO 飞机冒烟
        }

        /// <summary>
        /// 飞机死亡
        /// </summary>
        private void PlaneDie() {
            // TODO 飞机死亡
        }

    }
}