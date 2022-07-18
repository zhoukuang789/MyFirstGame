using System.Collections.Generic;
using Airplane.Bot.Moveset;
using Airplane.Bot.MovesetAction;
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

        private Moveset.Moveset currentMoveset = new DefaultMoveset();


        // -------------------mono method ----------------------
        private void Awake() {
            plane = GetComponent<PlaneBehaviour>();
            currentMovesetActionQueue = new Queue<MovesetAction.MovesetAction>();
        }

        private void Start() {
            
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

        /// <summary>
        /// 改变当前的Moveset
        /// </summary>
        /// <param name="moveset"></param>
        public void ChangeMoveset(Moveset.Moveset moveset) {
            if (currentMovesetActionQueue.Count != 0) {
                return;
            }
            currentMoveset = moveset;
            currentMovesetActionQueue.Clear();
            foreach (MovesetAction.MovesetAction movesetAction in currentMoveset.GetMovesetActionQueue()) {
                currentMovesetActionQueue.Enqueue(movesetAction);
            }
            // currentMovesetActionQueue.Enqueue(new FlyForward(100f, transform.position));
        }

        public PlaneBehaviour GetPlane() {
            return plane;
        }

    }
}