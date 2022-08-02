using UnityEngine;
using Valve.VR;

namespace MyVR {
    public class VRBehaviour : MonoBehaviour {
        public Transform cam;

        private SteamVR_TrackedObject trackedObj;
        

        private void Start() {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }

        private void Update() {
            
        }
    }
}