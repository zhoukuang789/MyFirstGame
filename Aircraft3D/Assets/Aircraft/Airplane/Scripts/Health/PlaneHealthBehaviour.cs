using System;
using Record;
using UnityEngine;
using UnityStandardAssets.Effects;

namespace Airplane.Health {
    public class PlaneHealthBehaviour : MonoBehaviour {
        
        // --------------------field -------------------------------
        private PlaneBehaviour plane;
        
        /// <summary>
        /// 飞机生命值
        /// </summary>
        private float health = 100f;
        
        /// <summary>
        /// 子弹击中特效
        /// </summary>
        private GameObject planeOnHitVfxPrefab;
        
        /// <summary>
        /// 飞机死亡特效
        /// </summary>
        private GameObject planeDeathVfxPrefab;
        
        /// <summary>
        /// 冒烟特效
        /// </summary>
        private GameObject damageSmokeVfxPrefab;
        
        /// <summary>
        /// 飞机冒烟位置
        /// </summary>
        public Transform smokeTransform;
        
        /// <summary>
        /// 冒烟特效
        /// </summary>
        private GameObject damageSmokeVfx;

        /// <summary>
        /// 是否冒烟
        /// </summary>
        private bool isSmoke;
        
        // ----------------mono method ----------------------------
        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
            planeOnHitVfxPrefab = Resources.Load<GameObject>("VFX/PlaneOnHit");
            planeDeathVfxPrefab = Resources.Load<GameObject>("VFX/PlaneDeath");
            damageSmokeVfxPrefab = Resources.Load<GameObject>("VFX/DamageSmoke");
        }

        private void Update() {
            // 烟雾特效跟随飞机
            if (isSmoke) {
                damageSmokeVfx.transform.position = smokeTransform.position;
                damageSmokeVfx.transform.rotation = smokeTransform.rotation * smokeTransform.rotation *
                                                    Quaternion.AngleAxis(180, -smokeTransform.right);
            }
        }

        // -----------------getter & setter -------------------------
        
        /// <summary>
        /// 减少生命值，如果生命值低于30%，飞机冒烟；生命值低于0，飞机死亡
        /// </summary>
        /// <param name="damage"></param>
        public void ReduceHealth(float damage) {
            health -= damage;
            if (health <= health * 0.3f && !isSmoke) {
                Smoke();
                isSmoke = true;
            }
            if (health <= 0f) {
                PlaneDie();
            }
        }
        
        // ---------------------function ----------------------------

        /// <summary>
        /// 飞机冒烟
        /// </summary>
        private void Smoke() {
            // 生成烟雾特效
            damageSmokeVfx = Instantiate(damageSmokeVfxPrefab, smokeTransform.position, smokeTransform.rotation * Quaternion.AngleAxis(180, -smokeTransform.right));
            damageSmokeVfx.SetActive(true);
        }

        /// <summary>
        /// 飞机死亡
        /// </summary>
        private void PlaneDie() {
            // 生成爆炸特效
            GameObject planeDeathVfx = Instantiate(planeDeathVfxPrefab, transform.position, transform.rotation);
            planeDeathVfx.SetActive(true);
            // 广播死亡事件
            PlaneHealthService.GetInstance().SetPlane(plane).PublishPlaneDeathEvent();
            // 销毁该飞机
            Destroy(damageSmokeVfx);
            Destroy(gameObject);
        }

    }
}