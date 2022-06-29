using UnityEngine;

namespace Airplane.Bot.MovesetAction {
    public class PlanePosture {
        
        private Transform transform;
        
        public PlanePosture() {
        }
        
        public PlanePosture(Transform transform) {
            this.transform = transform;
        }
        
        public Transform GetTransform() {
            return transform;
        }

        public PlanePosture SetTransform(Transform transform) {
            this.transform = transform;
            return this;
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public float GetPitchAngle() {
            Vector3 forwardOnYZ = Vector3.ProjectOnPlane(transform.forward, Vector3.right);
            return Vector3.SignedAngle(forwardOnYZ, Vector3.forward, transform.right);
        }

        public float GetRollAngle() {
            Vector3 rightOnXY = Vector3.ProjectOnPlane(transform.right, Vector3.forward);
            return Vector3.SignedAngle(rightOnXY, Vector3.right, transform.forward);
        }
        
    }
}