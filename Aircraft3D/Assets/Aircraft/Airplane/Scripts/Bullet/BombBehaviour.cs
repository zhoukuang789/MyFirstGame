using Airplane.Health;
using UnityEngine;

namespace Airplane.Bullet {
    public class BombBehaviour : MonoBehaviour {
        
        
        // -------------------field------------------------------------------

        private Rigidbody rb;

        private PlaneCamp camp;
        
        /// <summary>
        /// 武器伤害
        /// </summary>
        private float damage;

        /// <summary>
        /// 炸弹移动速度
        /// </summary>
        private float speed;
        
        /// <summary>
        /// 轰炸目标
        /// </summary>
        private Transform target;

        // --------------------------mono method----------------------------------------
        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            // 移动
            Vector3 speedVec = target.position - transform.position;
            rb.MovePosition(transform.position + (speedVec.normalized * speed) * Time.fixedDeltaTime);
            // 自动销毁
            // passedRange += Vector3.Distance(transform.position, lastPosition);
            // if (passedRange >= range) {
            //     Destroy(gameObject);
            // }
        }
        
        // ---------------------------function---------------------------------------------
        
        
        
        // --------------------------getter & setter----------------------------------------

        public BombBehaviour SetCamp(PlaneCamp camp) {
            this.camp = camp;
            return this;
        }
        
        public BombBehaviour SetDamage(float damage) {
            this.damage = damage;
            return this;
        }

        public BombBehaviour SetTarget(Transform target) {
            this.target = target;
            return this;
        }

        public BombBehaviour SetSpeed(float speed) {
            this.speed = speed;
            return this;
        }
        
    }
}