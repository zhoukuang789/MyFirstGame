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
            Vector3 forwardOnHorizontal = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            return Vector3.SignedAngle(transform.forward, forwardOnHorizontal, transform.right);
        }

        public float GetRollAngle() {
            Vector3 rightOnHorizontal = Vector3.ProjectOnPlane(transform.right, Vector3.up);
            return Vector3.SignedAngle(transform.right, rightOnHorizontal, transform.forward);
        }
        
    }
}