using System;
using Airplane.Health;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private Vector3 targetPos;


        float timeToDie;
        // --------------------------mono method----------------------------------------
        private void Awake() {
            rb = GetComponent<Rigidbody>();
        }

        private void Start() {
            timeToDie = Time.time + 7f;
        }

        private void FixedUpdate() {
            //移动
            Vector3 speedVec = targetPos - transform.position;
            rb.MovePosition(transform.position + (speedVec.normalized * speed) * Time.fixedDeltaTime);
            // 自动销毁
            // passedRange += Vector3.Distance(transform.position, lastPosition);
            // if (passedRange >= range) {
            //     Destroy(gameObject);
            // }

            if (Time.time>timeToDie)
            {
                Destroy(gameObject);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            var cb = other.GetComponent<ConstructureBehaviour>();
            if (cb != null)
            {
                //Debug.Log("对敌人造成伤害" + damgeValue);
                // com.SoundService.instance.Play("exp" + Random.Range(1, 3));
                cb.ReceiveDamage(damage);
                Destroy(gameObject);
            }
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

        public BombBehaviour SetTarget(Vector3 pos) {
            this.targetPos = pos;
            return this;
        }

        public BombBehaviour SetSpeed(float speed) {
            this.speed = speed;
            return this;
        }
        
    }
}