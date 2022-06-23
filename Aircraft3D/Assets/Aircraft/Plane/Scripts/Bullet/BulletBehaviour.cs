using System;
using UnityEngine;

namespace Plane.Bullet {
    public class BulletBehaviour : MonoBehaviour {
        
        // -------------------field------------------------------------------

        private Rigidbody rb;

        private Camp camp;
        
        /// <summary>
        /// 武器伤害
        /// </summary>
        private float damage;
        
        /// <summary>
        /// 子弹射程
        /// </summary>
        private float range;

        /// <summary>
        /// 子弹初速度
        /// </summary>
        private float initialSpeed;

        /// <summary>
        /// 子弹飞行速度
        /// </summary>
        private float flightSpeed;

        /// <summary>
        /// 经过的射程
        /// </summary>
        private float passedRange;

        /// <summary>
        /// 上一帧的位置
        /// </summary>
        private Vector3 lastPosition;

        // --------------------------mono method----------------------------------------
        private void Awake() {
            rb = GetComponent<Rigidbody>();
            
            
        }

        private void Start() {
            passedRange = 0f;
            lastPosition = transform.position;
        }

        private void FixedUpdate() {
            // 移动
            rb.MovePosition(transform.position + (transform.forward * (initialSpeed + flightSpeed)) * Time.fixedDeltaTime);
            // 自动销毁
            passedRange += Vector3.Distance(transform.position, lastPosition);
            if (passedRange >= range) {
                Destroy(gameObject);
            }
            
            // 射线检测
            // 用射线检测，子弹的碰撞，当物体在两点之间产生的射线内时，算作发生碰撞
            // 1.获得射线的起点（原点）
            Vector3 origin = transform.position;
            // 2.获得射线的方向
            Vector3 direction = transform.position - lastPosition;
            // 3.获得射线的距离
            float maxDistance = (transform.position - lastPosition).magnitude;
            //存储碰撞信息
            RaycastHit hit;
            //光线投射，检测是否发生碰撞
            bool isCollider = Physics.Raycast(origin, direction, out hit, maxDistance);
            if (isCollider) {
                //射线检测到物体，执行以下动作
                Camp hitCamp = hit.collider.gameObject.GetComponentInParent<PlaneBehaviour>().camp;
                if (hitCamp != camp) {
                    hit.collider.gameObject.GetComponentInParent<global::PlaneHealth>()?.ReceiveDamage(damage);
                }
            }
            
            lastPosition = transform.position;
        }
        
        // ---------------------------function---------------------------------------------
        
        
        
        // --------------------------getter & setter----------------------------------------

        public BulletBehaviour SetCamp(Camp camp) {
            this.camp = camp;
            return this;
        }
        
        public BulletBehaviour SetDamage(float damage) {
            this.damage = damage;
            return this;
        }

        public BulletBehaviour SetRange(float range) {
            this.range = range;
            return this;
        }

        public BulletBehaviour SetInitialSpeed(float initialSpeed) {
            this.initialSpeed = initialSpeed;
            return this;
        }

        public BulletBehaviour SetFlightSpeed(float flightSpeed) {
            this.flightSpeed = flightSpeed;
            return this;
        }
        
    }
}