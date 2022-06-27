using System;
using System.Collections;
using Plane.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Plane {
    public class BotController : MonoBehaviour {

        // -------------------field ----------------------------
        private PlaneBehaviour plane;

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
            TurnLeft();
        }

        private void Update() {
            Vector3 forwardOnYZ = Vector3.ProjectOnPlane(transform.forward, Vector3.right);
            pitchAngle = Vector3.SignedAngle(forwardOnYZ, Vector3.forward, transform.right);
            
            Vector3 rightOnXY = Vector3.ProjectOnPlane(transform.right, Vector3.forward);
            rollAngle = Vector3.SignedAngle(rightOnXY, Vector3.right, transform.forward);
            
            // 恢复飞行姿态
            if (isRestorePosture) {
                Debug.Log("恢复飞行姿态中");
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
                    isRestorePosture = false;
                }
            }

            // 往前直飞
            if (isDirectFly  && !isRestorePosture) {
                Debug.Log("直飞中");
                if (Vector3.Distance(positionBeforeDirectFly, transform.position) < directFlyDistance) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
                } else {
                    isDirectFly = false;
                }
            }
            
            // 向上俯仰
            if (isPitchUp && !isRestorePosture && !isRollRight && !isRollRight) {
                Debug.Log("向上俯仰中");
                if (pitchUpAngle > 0f) {
                    pitchUpAngle -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
                } else {
                    isPitchUp = false;
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
        /// 恢复飞行姿态
        /// </summary>
        public void RestorePosture() {
            isRestorePosture = true;
        }
        
        /// <summary>
        /// 直飞 distance 米
        /// </summary>
        public void DirectFly(float distance) {
            positionBeforeDirectFly = transform.position;
            directFlyDistance = distance;
            isDirectFly = true;
        }

        /// <summary>
        /// 向上俯仰 angle 度
        /// </summary>
        /// <param name="angle"></param>
        public void PitchUp(float angle) {
            pitchUpAngle = angle;
            isPitchUp = true;
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
            RestorePosture();
            PitchUp(angle);
            DirectFly(altitude / Mathf.Sin(angle * Mathf.Deg2Rad));
            RestorePosture();
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