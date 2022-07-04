using Airplane.Movement;
using UnityEngine;

namespace Airplane.Bot.MovesetAction {
    public class RollRight : MovesetAction {
        public RollRight(float value) {
            this.value = value;
        }
        
        public override void DoAction(PlaneBehaviour plane) {
            // Debug.Log("MovesetAction RollRight");
            if (value > 0f) {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
                value -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
            } else {
                // 如果达标
                isComplete = true;
            }
        }
    }
}