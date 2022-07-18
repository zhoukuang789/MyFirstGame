using System;
using System.Collections;
using System.Collections.Generic;
using Airplane.Bot.Moveset;
using Airplane.Weapon;
using UnityEngine;

namespace Airplane.Bot {
    public class FighterDecision : MonoBehaviour {
        //---------------------field ----------------------------
        private BotBehaviour bot;

        public Transform target;

        /// <summary>
        /// 最佳Moveset
        /// </summary>
        private Moveset.Moveset bestMoveset;

        //--------------------- mono method ---------------------
        private void Awake() {
            bot = GetComponent<BotBehaviour>();
        }

        private void Start() {
            target = GameObject.Find("PlayerPlane").transform;
            // 每05秒做一次决策
            StartCoroutine(MakeDecision(0.5f));
        }

        private void Update() {
            if (target == null) {
                return;
            }
            if (Vector3.Angle(transform.forward, target.position - transform.position) < 30f &&
                Vector3.Distance(target.position, transform.position) < 150f) {
                PlaneWeaponControllerService.GetInstance().SetPlane(bot.GetPlane()).Fire();
            }
        }

        private IEnumerator MakeDecision(float seconds) {
            while (true) {
                //做一次决策
                Debug.Log("Bot Decision");
                
                // 计算当前最佳Moveset
                if (target == null) {
                    bestMoveset = new DefaultMoveset();
                    yield break;
                }
                bestMoveset = CalculateBestMoveset(bot.transform, target);
                bot.ChangeMoveset(bestMoveset);


                // 等待0.5秒做下一次决策
                yield return new WaitForSeconds(seconds);
            }
            // ReSharper disable once IteratorNeverReturns
        }


        private Moveset.Moveset CalculateBestMoveset(Transform botTransform, Transform targetTransform) {
            // 飞机到目标点的向量
            Vector3 deltaPosition = targetTransform.position - botTransform.position;
            // 飞机到目标点的向量在水平平面上的分量
            Vector3 deltaPositionOnHorizontal = Vector3.ProjectOnPlane(deltaPosition, Vector3.up);
            // 飞机forward在水平平面上的分量
            Vector3 forwardOnHorizontal = Vector3.ProjectOnPlane(botTransform.forward, Vector3.up);
            // 飞机forward与目标点的水平夹角
            float deltaAngleOnHorizontal = Vector3.SignedAngle(forwardOnHorizontal, deltaPositionOnHorizontal, Vector3.up);

            Vector3 deltaPositionOnVertical = Vector3.ProjectOnPlane(deltaPosition, Vector3.right);
            Vector3 forwardOnVertical = Vector3.ProjectOnPlane(botTransform.forward, Vector3.right);
            float deltaAngleOnVertical = Vector3.SignedAngle(deltaPositionOnVertical, forwardOnVertical, Vector3.right);

            
            if (deltaPositionOnVertical.y > 5f) {
                return new Climb(30f, Mathf.Abs(deltaPositionOnVertical.y), bot.transform.position);
            }
            if (deltaPositionOnVertical.y < -5f) {
                return new Dive(30f,  Mathf.Abs(deltaPositionOnVertical.y), bot.transform.position);
            }

            float distance = deltaPositionOnHorizontal.magnitude;
            float fixAngel = CalculateFixAngle(distance, 1f, 200f, 90f, 1f);
            if (deltaAngleOnHorizontal > 10f) {
                return new TurnRight(deltaAngleOnHorizontal + fixAngel, botTransform.position);
            }
            
            if (deltaAngleOnHorizontal < -10f) {
                return new TurnLeft(-deltaAngleOnHorizontal + fixAngel, botTransform.position);
            }
            
            
            return new StraightFly(5f, botTransform.position);
        }

        /// <summary>
        /// 计算转弯修正角度
        /// </summary>
        /// <param name="distance">(20, 30) (200, 5)</param>
        /// <returns></returns>
        private float CalculateFixAngle(float distance, float minDistance, float maxDistance, float fixAngleWhenMinDistance, float fixAngleWhenMaxDistance) {
            distance = Mathf.Clamp(distance, 20f, 200f);
            float k = (fixAngleWhenMaxDistance - fixAngleWhenMinDistance) / (maxDistance - minDistance);
            float b = fixAngleWhenMinDistance - k * minDistance;
            return k * distance + b;
        }
    }
}