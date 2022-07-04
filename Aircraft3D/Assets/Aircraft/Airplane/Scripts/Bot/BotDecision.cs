using System;
using System.Collections;
using System.Collections.Generic;
using Airplane.Bot.Moveset;
using UnityEngine;

namespace Airplane.Bot {
    public class BotDecision : MonoBehaviour {
        //---------------------field ----------------------------
        private BotBehaviour bot;

        public GameObject targets;

        public List<Transform> targetTransformList;

        private int cursor;

        private Transform currentTartgetTransform;

        /// <summary>
        /// 最佳Moveset
        /// </summary>
        private Moveset.Moveset bestMoveset;

        //--------------------- mono method ---------------------
        private void Awake() {
            bot = GetComponent<BotBehaviour>();
            targetTransformList = new List<Transform>();
            foreach (Transform target in targets.transform) {
                targetTransformList.Add(target);
            }
            cursor = 0;
            currentTartgetTransform = targetTransformList[cursor];
        }

        private void Start() {
            // 每05秒做一次决策
            StartCoroutine(MakeDecision(0.5f));
        }

        private IEnumerator MakeDecision(float seconds) {
            while (true) {
                //做一次决策
                
                
                // 到达目标点，切换下一个目标点
                if (Vector3.Distance(bot.transform.position, currentTartgetTransform.position) <= 20f) {
                    cursor++;
                    if (cursor > targetTransformList.Count - 1) {
                        cursor = 0;
                    }
                    currentTartgetTransform = targetTransformList[cursor];
                    continue;
                }
                // 计算当前最佳Moveset
                bestMoveset = CalculateBestMoveset(bot.transform, currentTartgetTransform);
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
        /// 向量 from 在向量 to 上的投影向量
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        private Vector3 ProjectOnVector(Vector3 from, Vector3 to) {
            //to的单位向量
            Vector3 unitNormalized = to.normalized;
            //投影向量的长度
            float length = Vector3.Dot(from, to) / to.magnitude;
            return length * unitNormalized;
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