using Airplane.Movement;
using UnityEngine;

namespace Airplane.Bot {
    public class RestorePosture : MovesetAction {
        
        
        public override void DoAction(PlaneBehaviour plane, float pitchAngle, float rollAngle) {
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
                isComplete = true;
            }
        }
        
    }
}