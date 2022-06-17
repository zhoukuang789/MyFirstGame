using System;
using UnityEngine;

namespace Plane {
    public class BotController : MonoBehaviour {
        
        private PlaneMoveBehaviour planeBehaviour;
        
        private void Start() {
            planeBehaviour = GetComponent<PlaneMoveBehaviour>();
        }

        private void Update() {
            PlaneControllerService.GetInstance().SetPlane(planeBehaviour).AddTrust(0.5f);
        }
    }
}