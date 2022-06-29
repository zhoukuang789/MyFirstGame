using Airplane.Movement;
using UnityEngine;

namespace Airplane.Bot.MovesetAction {
    
    public class PitchUp : MovesetAction {

        public PitchUp(float value) {
            this.value = value;
        }
        
        public override void DoAction(PlaneBehaviour plane) {
            if (value > 0f) {
                value -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
            } else {
                // 如果达标
                isComplete = true;
            }
        }
        
    }
    
}