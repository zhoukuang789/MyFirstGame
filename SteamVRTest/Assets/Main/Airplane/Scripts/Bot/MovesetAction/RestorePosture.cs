using Airplane.Movement;
using UnityEngine;

namespace Airplane.Bot.MovesetAction {
    public class RestorePosture : MovesetAction {
        
        
        public override void DoAction(PlaneBehaviour plane) {
            // Debug.Log("MovesetAction RestorePosture");
            PlanePosture currentPlanePosture = GetCurrentPlanePosture(plane);
            float rollAngle = currentPlanePosture.GetRollAngle();
            float pitchAngle = currentPlanePosture.GetPitchAngle();
            // Debug.Log("rollAngle, " + rollAngle);
            // Debug.Log("pitchAngle, " + pitchAngle);
            PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
            if (rollAngle > 5f) {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(-1);
            }
            if (rollAngle < -5f) {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(1);
            }
            if (pitchAngle > 5f) {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(-1);
            }
            if (pitchAngle < 0f) {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(1);
            }
            if (rollAngle >= -5f && rollAngle <= 5f && pitchAngle >= 0f && pitchAngle <= 5f) {
                // 如果达标
                isComplete = true;
            }
        }
        
    }
}