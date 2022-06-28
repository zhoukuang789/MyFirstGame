using System.Numerics;

namespace Airplane.Bot {

    public class FlyForward : MovesetAction {
        
        public float positionBeforeDirectFly;
        
        public override void DoAction(PlaneBehaviour plane, float pitchAngle, float rollAngle) {
            // if (Vector3.Distance(positionBeforeDirectFly, plane.transform.position) < value) {
            //     PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
            // } else {
            //     // 如果达标
            //     currentMovesetActionQueue.Dequeue();
            //     Debug.Log(currentMovesetActionQueue.Count);
            //     if (currentMovesetActionQueue.Count != 0) {
            //         currentMovesetAction = currentMovesetActionQueue.Peek();
            //         Debug.Log(currentMovesetAction);
            //     } else {
            //         // currentMovesetAction = MovesetAction.None;
            //     }
            // }
        }
    }
    
}