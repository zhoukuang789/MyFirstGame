using Airplane.Movement;
using UnityEngine;

namespace Airplane.Bot {
    
    public class PitchUp : MovesetAction {
        
        public override void DoAction(PlaneBehaviour plane, float pitchAngle, float rollAngle) {
            if (value > 0f) {
                value -= PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
            } else {
                // 如果达标
                isComplete = true;
            }
        }
        
    }
    
}