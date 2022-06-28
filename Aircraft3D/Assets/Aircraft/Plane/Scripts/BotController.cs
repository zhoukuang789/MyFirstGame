using System;
using System.Collections;
using Plane.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Plane {
    public class BotController : MonoBehaviour {

        // -------------------field ----------------------------
        private PlaneBehaviour plane;

        enum CurrentAction {
            None,   // 无行为
            RestorePosture, // 恢复姿态
            Pitch,  // 俯仰
            Roll,   // 滚转
            DirectFly   // 直飞
        }

        /// <summary>
        /// 当前行为
        /// </summary>
        private CurrentAction currentAction = CurrentAction.None;

        private int actionNum = 0;

        public float pitchAngle;

        public float rollAngle;

        /// <summary>
        /// 是否正在恢复飞行姿态
        /// </summary>
        private bool isRestorePosture;

        /// <summary>
        /// 是否正在往前直飞
        /// </summary>
        private bool isDirectFly;
        private float directFlyDistance;
        private Vector3 positionBeforeDirectFly;

        /// <summary>
        /// 是否正在向上俯仰
        /// </summary>
        private bool isPitchUp;
        /// <summary>
        /// 向上俯仰的角度
        /// </summary>
        private float pitchUpAngle;

        private bool isPitchDown;
        private float pitchDownAngle;
        
        private bool isRollRight;
        private float rollRightAngle;
        
        private bool isRollLeft;
        private float rollLeftAngle;


        // -------------------mono mathod ----------------------
        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
        }

        private void Start() {
            Climb(30f, 100f);
        }

        private void Update() {
            Vector3 forwardOnYZ = Vector3.ProjectOnPlane(transform.forward, Vector3.right);
            pitchAngle = Vector3.SignedAngle(forwardOnYZ, Vector3.forward, transform.right);
            
            Vector3 rightOnXY = Vector3.ProjectOnPlane(transform.right, Vector3.forward);
            rollAngle = Vector3.SignedAngle(rightOnXY, Vector3.right, transform.forward);
            
            // 恢复飞行姿态
            if (currentAction == CurrentAction.RestorePosture) {
                actionNum++;
                if (rollAngle > 2f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(-1);
                }
                if (rollAngle < -2f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
                }
                if (pitchAngle > 2f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
                }
                if (pitchAngle < 0f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
                }

                if (rollAngle >= -2f && rollAngle <= 2f && pitchAngle >= 0f && pitchAngle <= 2f) {
                    currentAction = CurrentAction.None;
                    actionNum--;
                }
            }

            // 往前直飞
            if (currentAction == CurrentAction.DirectFly) {
                actionNum++;
                if (Vector3.Distance(positionBeforeDirectFly, transform.position) < directFlyDistance) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
                } else {
                    currentAction = CurrentAction.None;
                    actionNum--;
                }
            }
            
            // 向上俯仰
            if (currentAction == CurrentAction.Pitch) {
                actionNum++;
                if (pitchUpAngle > 0f) {
                    pitchUpAngle -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
                } else {
                    currentAction = CurrentAction.None;
                    actionNum--;
                }
            }
            
            // 向下俯仰
            if (isPitchDown && !isRestorePosture && !isRollRight && !isRollRight) {
                Debug.Log("向下俯仰中");
                if (pitchDownAngle > 0f) {
                    pitchDownAngle += PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(-1);
                } else {
                    isPitchDown = false;
                }
            }
            
            // 向右滚转
            if (isRollRight && !isRestorePosture) {
                Debug.Log("向右滚转中");
                if (rollRightAngle > 0f) {
                    rollRightAngle -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
                } else {
                    isRollRight = false;
                }
            }
            
            // 向左滚转
            if (isRollLeft && !isRestorePosture) {
                Debug.Log("向左滚转中");
                if (rollLeftAngle > 0f) {
                    rollLeftAngle += PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(-1);
                } else {
                    isRollLeft = false;
                }
            }
        }

        // ---------------------function -----------------------

        /// <summary>
        /// 等待当前动作变为None
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitOtherAction() {
            while (actionNum > 0) {
                yield return null;
            }
        }
        
        /// <summary>
        /// 恢复飞行姿态
        /// </summary>
        public IEnumerator RestorePosture() {
            yield return WaitOtherAction();
            Debug.Log("恢复飞行姿态中");
            currentAction = CurrentAction.RestorePosture;
        }
        
        /// <summary>
        /// 直飞 distance 米
        /// </summary>
        public IEnumerator DirectFly(float distance) {
            yield return WaitOtherAction();
            Debug.Log("直飞中");
            positionBeforeDirectFly = transform.position;
            directFlyDistance = distance;
            currentAction = CurrentAction.DirectFly;
        }

        /// <summary>
        /// 向上俯仰 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public IEnumerator PitchUp(float angle) {
            yield return WaitOtherAction();
            Debug.Log("向上俯仰中");
            pitchUpAngle = angle;
            currentAction = CurrentAction.Pitch;
        }

        /// <summary>
        /// 向下俯仰 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void PitchDown(float angle) {
            pitchDownAngle = angle;
            isPitchDown = true;
        }

        /// <summary>
        /// 向右滚转 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void RollRight(float angle) {
            rollRightAngle = angle;
            isRollRight = true;
        }
        
        /// <summary>
        /// 向左滚转 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void RollLeft(float angle) {
            rollLeftAngle = angle;
            isRollLeft = true;
        }
        

        /// <summary>
        /// 以 angle 俯仰度爬升 altitude 米
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="altitude"></param>
        public void Climb(float angle, float altitude) {
            StartCoroutine(RestorePosture());
            StartCoroutine(PitchUp(angle));
            StartCoroutine(DirectFly(altitude / Mathf.Sin(angle * Mathf.Deg2Rad)));
            StartCoroutine(RestorePosture());
        }

        /// <summary>
        /// 俯冲
        /// </summary>
        public void Dive() {
        }

        /// <summary>
        /// 左转弯
        /// </summary>
        public void TurnLeft() {
            RestorePosture();
            RollLeft(90f);
            PitchUp(90f);
            DirectFly(100);
            RestorePosture();
        }

        /// <summary>
        /// 右转弯
        /// </summary>
        public void TurnRight() {
            
        }
    }
}