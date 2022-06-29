using System.Collections.Generic;
using Airplane.Bot.Moveset;
using UnityEngine;

namespace Airplane.Bot {
    
    public class BotBehaviour : MonoBehaviour {

        // -------------------field ----------------------------
        private PlaneBehaviour plane;

        /// <summary>
        /// 当前执行的动作
        /// </summary>
        private MovesetAction.MovesetAction currentMovesetAction;

        /// <summary>
        /// 当前执行动作的队列
        /// </summary>
        private Queue<MovesetAction.MovesetAction> currentMovesetActionQueue;


        // -------------------mono method ----------------------
        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
            currentMovesetActionQueue = new Queue<MovesetAction.MovesetAction>();
        }

        private void Start() {
            Moveset.Moveset moveset = new Climb(30f, 100f, plane.transform.position);
            foreach (MovesetAction.MovesetAction movesetAction in moveset.GetMovesetActionQueue()) {
                currentMovesetActionQueue.Enqueue(movesetAction);
            }
        }

        
        private void Update() {
            if (CheckObstacle()) {
                // 避开障碍
                return;
            }
            if (currentMovesetActionQueue.Count == 0) {
                return;
            }
            currentMovesetAction = currentMovesetActionQueue.Peek();
            if (!currentMovesetAction.GetIsComplete()) {
                currentMovesetAction.DoAction(plane);
            } else {
                currentMovesetActionQueue.Dequeue();
            }

            
            
            
            // // 向下俯仰
            // if (currentMovesetAction == MovesetAction.PitchDown) {
            //     if (pitchDownAngle > 0f) {
            //         pitchDownAngle += PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(-1);
            //     } else {
            //         // 如果达标
            //         currentMovesetActionQueue.Dequeue();
            //         Debug.Log(currentMovesetActionQueue.Count);
            //         if (currentMovesetActionQueue.Count != 0) {
            //             currentMovesetAction = currentMovesetActionQueue.Peek();
            //             Debug.Log(currentMovesetAction);
            //         } else {
            //             // currentMovesetAction = MovesetAction.None;
            //         }
            //     }
            // }
            //
            // // 向右滚转
            // if (currentMovesetAction == MovesetAction.RollRight) {
            //     Debug.Log("向右滚转中");
            //     if (rollRightAngle > 0f) {
            //         rollRightAngle -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
            //     } else {
            //         // 如果达标
            //         currentMovesetActionQueue.Dequeue();
            //         Debug.Log(currentMovesetActionQueue.Count);
            //         if (currentMovesetActionQueue.Count != 0) {
            //             currentMovesetAction = currentMovesetActionQueue.Peek();
            //             Debug.Log(currentMovesetAction);
            //         } else {
            //             // currentMovesetAction = MovesetAction.None;
            //         }
            //     }
            // }
            //
            // // 向左滚转
            // if (currentMovesetAction == MovesetAction.RollLeft) {
            //     Debug.Log("向左滚转中");
            //     if (rollLeftAngle > 0f) {
            //         rollLeftAngle += PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(-1);
            //     } else {
            //         // 如果达标
            //         currentMovesetActionQueue.Dequeue();
            //         Debug.Log(currentMovesetActionQueue.Count);
            //         if (currentMovesetActionQueue.Count != 0) {
            //             currentMovesetAction = currentMovesetActionQueue.Peek();
            //             Debug.Log(currentMovesetAction);
            //         } else {
            //             // currentMovesetAction = MovesetAction.None;
            //         }
            //     }
            // }
        }
        
        // --------------------function ---------------------------------
        private bool CheckObstacle() {
            return false;
        }
        
        // -------------------getter & setter ----------------------------
        public PlaneBehaviour GetPlane() {
            return plane;
        }

    }
}