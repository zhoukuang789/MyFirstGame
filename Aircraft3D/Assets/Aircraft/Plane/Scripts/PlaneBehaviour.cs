using System;
using UnityEngine;

namespace Plane {
    public class PlaneBehaviour : MonoBehaviour {

        public PlaneConfig planeConfig;
        
        public bool isPlaneMovementWork = true;
        
        private Rigidbody rb;

        private Movement.PlaneMovement planeMovement;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            rb.mass = planeConfig.mass;
            planeMovement = new Movement.PlaneMovement(planeConfig.planeMovementConfig, rb, transform);
        }

        private void Update() {
            if (isPlaneMovementWork) {
                planeMovement.Update();
            } else {
                rb.useGravity = false;
            }
        }

        private void FixedUpdate() {
            if (isPlaneMovementWork) {
                planeMovement.FixedUpdate();
            } else {
                rb.useGravity = false;
            }
        }
        
        // ------------------------------Getter & Setter----------------------------

        public Rigidbody GetRigidbody() {
            return rb;
        }
        
        public Movement.PlaneMovement GetPlaneMovement() {
            return planeMovement;
        }
    }
}