using System.Diagnostics;
using UnityEngine;

namespace MyCamera
{
    public class CameraBehaviour : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;


        public float distanceLerpDuration = 2;
        public float startDistance;
        public float endDistance;
        public float lookAtDistance;
        float _distanceTimer;
        float _currentDistance;

        public CameraTrackingMode currentTrackingMode = CameraTrackingMode.HorizontalTracking;
        private float endTime;
        private Transform spotTarget;

        [Header("SpotTracking")]
        public Transform spot;

        private void LateUpdate()
        {
            if (target == null)
                return;

            switch (currentTrackingMode) {
                case CameraTrackingMode.HorizontalTracking:
                    SyncDistance();
                    transform.position = target.position + (-target.forward * offset.z + target.up * offset.y).normalized * _currentDistance;
                    transform.LookAt(target.position + target.forward * lookAtDistance, Vector3.up);
                    break;
                case CameraTrackingMode.SpotTracking:
                    if (endTime < Time.time) {
                        currentTrackingMode = CameraTrackingMode.HorizontalTracking;
                        break;
                    }
                    transform.position = spot.position;
                    transform.LookAt(spotTarget.position, Vector3.up);
                    break;
            }
            
        }

        void SyncDistance()
        {
            if (_distanceTimer > distanceLerpDuration)
                return;

            _distanceTimer += Time.deltaTime;
            var r = _distanceTimer / distanceLerpDuration;
            if (r > 1)
                r = 1;

            _currentDistance = Mathf.Lerp(startDistance, endDistance, r);
        }

        public void ChangeTrackingMode(CameraTrackingMode cameraTrackingMode, float duration, Transform spotTarget) {
            currentTrackingMode = cameraTrackingMode;
            endTime = Time.time + duration;
            this.spotTarget = spotTarget;
        }
    }
}