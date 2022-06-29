using Airplane.Movement;
using UnityEngine;

namespace Airplane.Bot.MovesetAction {

    public class FlyForward : MovesetAction {

        private readonly Vector3 positionBeforeAction;

        /// <summary>
        /// 往前飞的距离，如果value《0 表示一直往前飞
        /// </summary>
        /// <param name="value"></param>
        /// <param name="positionBeforeAction"></param>
        public FlyForward(float value, Vector3 positionBeforeAction) {
            this.value = value;
            this.positionBeforeAction = positionBeforeAction;
        }
        
        public override void DoAction(PlaneBehaviour plane) {
            if (value < 0) {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
            } else {
                Vector3 currentPosition = GetCurrentPlanePosture(plane).GetPosition();
                if (Vector3.Distance(positionBeforeAction, currentPosition) < value) {
                    Debug.Log(Vector3.Distance(positionBeforeAction, currentPosition));
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
                } else {
                    // 如果达标
                    isComplete = true;
                }
            }
        }
    }
    
}