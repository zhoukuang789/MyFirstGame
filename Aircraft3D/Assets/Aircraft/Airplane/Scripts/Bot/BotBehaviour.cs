using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Airplane.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Airplane.Bot {
    
    public class BotBehaviour : MonoBehaviour {

        // -------------------field ----------------------------
        private PlaneBehaviour plane;

        /// <summary>
        /// 当前行为
        /// </summary>
        private MovesetAction currentMovesetAction;

        /// <summary>
        /// 当前行为队列
        /// </summary>
        private Queue<MovesetAction> currentMovesetActionQueue;

        /// <summary>
        /// 飞机当前俯仰角
        /// </summary>
        public float pitchAngle;

        /// <summary>
        /// 飞机当前滚转角
        /// </summary>
        public float rollAngle;

        /// <summary>
        /// 往前飞达标距离
        /// </summary>
        private float directFlyDistance;
        
        /// <summary>
        /// 往前飞之前的位置
        /// </summary>
        private Vector3 positionBeforeDirectFly;

        /// <summary>
        /// 向上俯仰的达标角度
        /// </summary>
        private float pitchUpAngle;

        /// <summary>
        /// 向下飞的达标角度
        /// </summary>
        private float pitchDownAngle;
        
        /// <summary>
        /// 右滚转达标角度
        /// </summary>
        private float rollRightAngle;
        
        /// <summary>
        /// 左滚转达标角度
        /// </summary>
        private float rollLeftAngle;


        // -------------------mono mathod ----------------------
        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
        }

        private void Start() {
            Moveset moveset = new Climb();
            currentMovesetActionQueue = moveset.GetMovesetActionQueue();
            Debug.Log(currentMovesetActionQueue.Count);
            currentMovesetAction = currentMovesetActionQueue.Peek();
            Debug.Log(currentMovesetAction);
        }

        /**
        private void Update() {
            Vector3 forwardOnYZ = Vector3.ProjectOnPlane(transform.forward, Vector3.right);
            pitchAngle = Vector3.SignedAngle(forwardOnYZ, Vector3.forward, transform.right);
            
            Vector3 rightOnXY = Vector3.ProjectOnPlane(transform.right, Vector3.forward);
            rollAngle = Vector3.SignedAngle(rightOnXY, Vector3.right, transform.forward);
            
            // 恢复飞行姿态
            if (currentMovesetAction == MovesetAction.RestorePosture) {
                if (rollAngle > 2f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(-1);
                }
                if (rollAngle < -2f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
                }
                if (pitchAngle > 2f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(-1);
                }
                if (pitchAngle < 0f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
                }

                if (rollAngle >= -2f && rollAngle <= 2f && pitchAngle >= 0f && pitchAngle <= 2f) {
                    // 如果达标
                    currentMovesetActionQueue.Dequeue();
                    Debug.Log(currentMovesetActionQueue.Count);
                    if (currentMovesetActionQueue.Count != 0) {
                        currentMovesetAction = currentMovesetActionQueue.Peek();
                        Debug.Log(currentMovesetAction);
                    } else {
                        // currentMovesetAction = MovesetAction.None;
                    }
                }
            }

            // 往前直飞
            if (currentMovesetAction == MovesetAction.DirectFly) {
                if (Vector3.Distance(positionBeforeDirectFly, transform.position) < directFlyDistance) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
                } else {
                    // 如果达标
                    currentMovesetActionQueue.Dequeue();
                    Debug.Log(currentMovesetActionQueue.Count);
                    if (currentMovesetActionQueue.Count != 0) {
                        currentMovesetAction = currentMovesetActionQueue.Peek();
                        Debug.Log(currentMovesetAction);
                    } else {
                        // currentMovesetAction = MovesetAction.None;
                    }
                }
            }
            
            // 向上俯仰
            if (currentMovesetAction == MovesetAction.PitchUp) {
                if (pitchUpAngle > 0f) {
                    pitchUpAngle -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
                } else {
                    // 如果达标
                    currentMovesetActionQueue.Dequeue();
                    Debug.Log(currentMovesetActionQueue.Count);
                    if (currentMovesetActionQueue.Count != 0) {
                        currentMovesetAction = currentMovesetActionQueue.Peek();
                        Debug.Log(currentMovesetAction);
                    } else {
                        // currentMovesetAction = MovesetAction.None;
                    }
                }
            }
            
            // 向下俯仰
            if (currentMovesetAction == MovesetAction.PitchDown) {
                if (pitchDownAngle > 0f) {
                    pitchDownAngle += PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(-1);
                } else {
                    // 如果达标
                    currentMovesetActionQueue.Dequeue();
                    Debug.Log(currentMovesetActionQueue.Count);
                    if (currentMovesetActionQueue.Count != 0) {
                        currentMovesetAction = currentMovesetActionQueue.Peek();
                        Debug.Log(currentMovesetAction);
                    } else {
                        // currentMovesetAction = MovesetAction.None;
                    }
                }
            }
            
            // 向右滚转
            if (currentMovesetAction == MovesetAction.RollRight) {
                Debug.Log("向右滚转中");
                if (rollRightAngle > 0f) {
                    rollRightAngle -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
                } else {
                    // 如果达标
                    currentMovesetActionQueue.Dequeue();
                    Debug.Log(currentMovesetActionQueue.Count);
                    if (currentMovesetActionQueue.Count != 0) {
                        currentMovesetAction = currentMovesetActionQueue.Peek();
                        Debug.Log(currentMovesetAction);
                    } else {
                        // currentMovesetAction = MovesetAction.None;
                    }
                }
            }
            
            // 向左滚转
            if (currentMovesetAction == MovesetAction.RollLeft) {
                Debug.Log("向左滚转中");
                if (rollLeftAngle > 0f) {
                    rollLeftAngle += PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(-1);
                } else {
                    // 如果达标
                    currentMovesetActionQueue.Dequeue();
                    Debug.Log(currentMovesetActionQueue.Count);
                    if (currentMovesetActionQueue.Count != 0) {
                        currentMovesetAction = currentMovesetActionQueue.Peek();
                        Debug.Log(currentMovesetAction);
                    } else {
                        // currentMovesetAction = MovesetAction.None;
                    }
                }
            }
        }
        **/

        // ---------------------function -----------------------

        
        /// <summary>
        /// 恢复飞行姿态
        /// </summary>
        public void RestorePosture() {
            Debug.Log("恢复飞行姿态中");
            // currentMovesetAction = MovesetAction.RestorePosture;
        }
        
        /// <summary>
        /// 直飞 distance 米
        /// </summary>
        public void DirectFly(float distance) {
            Debug.Log("直飞中");
            positionBeforeDirectFly = transform.position;   // 记录当前位置
            directFlyDistance = distance;   // 达标距离
            // currentMovesetAction = MovesetAction.DirectFly;
        }

        /// <summary>
        /// 向上俯仰 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void PitchUp(float angle) {
            Debug.Log("向上俯仰中");
            pitchUpAngle = angle;   // 达标角度
            // currentMovesetAction = MovesetAction.PitchUp;
        }

        /// <summary>
        /// 向下俯仰 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void PitchDown(float angle) {
            pitchDownAngle = angle; // 达标角度
            // currentMovesetAction = MovesetAction.PitchDown;
        }

        /// <summary>
        /// 向右滚转 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void RollRight(float angle) {
            rollRightAngle = angle; // 达标角度
            // currentMovesetAction = MovesetAction.RollRight;
        }
        
        /// <summary>
        /// 向左滚转 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void RollLeft(float angle) {
            rollLeftAngle = angle;  // 达标角度
            // currentMovesetAction = MovesetAction.RollLeft;
        }
    }
}