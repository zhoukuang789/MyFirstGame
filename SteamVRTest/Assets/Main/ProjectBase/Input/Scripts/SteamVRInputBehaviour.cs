using System;
using Airplane;
using Airplane.Movement;
using Airplane.Weapon;
using ProjectBase.SingletonBase;
using UnityEngine;
using Valve.VR;

namespace ProjectBase.Input {
    
    
    public class SteamVRInputBehaviour : MonoBehaviour {

        public SteamVR_Action_Vector2 leftRocker;

        public SteamVR_Action_Vector2 rightRocker;

        public SteamVR_Action_Boolean leftTrigger;
        
        public SteamVR_Action_Boolean rightTrigger;
        
        public SteamVR_Action_Boolean leftGrab;
        
        public SteamVR_Action_Boolean rightGrab;
        
        public SteamVR_Action_Boolean aKey;
        
        public SteamVR_Action_Boolean bKey;
        
        public SteamVR_Action_Boolean xKey;
        
        public SteamVR_Action_Boolean yKey;

        public PlaneBehaviour plane;

        private void FixedUpdate() {
            if (leftRocker.axis.x != 0f || leftRocker.axis.y != 0f) {
                Debug.Log("左摇杆: " + leftRocker.axis);
                if (leftRocker.axis.x > 0f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(leftRocker.axis.y);
                }
                if (leftRocker.axis.x < 0f) {
                    PlaneMovementControllerService.GetInstance().SetPlane(plane).ReduceTrust(-leftRocker.axis.y);
                }
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoYaw(-leftRocker.axis.x);
            }
            if (rightRocker.axis.x != 0f || rightRocker.axis.y != 0f) {
                Debug.Log("右摇杆: " + rightRocker.axis);
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoRoll(rightRocker.axis.x);
                PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(-rightRocker.axis.y);
            }
            if (leftTrigger.state) {
                Debug.Log("左扳机");
            }
            if (rightTrigger.state) {
                Debug.Log("右扳机");
                PlaneWeaponControllerService.GetInstance().SetPlane(plane).Fire();
            }
            if (leftGrab.state) {
                Debug.Log("左抓取");
            }
            if (rightGrab.state) {
                Debug.Log("右抓取");
            }
            if (aKey.state) {
                Debug.Log("A");
            }
            if (bKey.state) {
                Debug.Log("B");
            }
            if (xKey.state) {
                Debug.Log("X");
            }
            if (yKey.state) {
                Debug.Log("Y");
            }
        }
    }
    

}