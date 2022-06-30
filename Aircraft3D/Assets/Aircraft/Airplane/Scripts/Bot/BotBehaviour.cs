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
            Moveset.Moveset moveset1 = new Climb(30f, 100, plane.transform.position);
            Moveset.Moveset moveset2 = new TurnLeft();
            Moveset.Moveset moveset3 = new TurnBack();
            foreach (MovesetAction.MovesetAction movesetAction in moveset1.GetMovesetActionQueue()) {
                currentMovesetActionQueue.Enqueue(movesetAction);
            }
            foreach (MovesetAction.MovesetAction movesetAction in moveset2.GetMovesetActionQueue()) {
                currentMovesetActionQueue.Enqueue(movesetAction);
            }
            foreach (MovesetAction.MovesetAction movesetAction in moveset3.GetMovesetActionQueue()) {
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