using System;
using UnityEngine;

namespace MyCamera
{
    public class CameraBehaviour : MonoBehaviour
    {
        public Transform target;
        public Transform playerPlane;
        public Vector3 offset;

        public float distanceLerpDuration = 2;
        public float startDistance;
        public float endDistance;
        public float lookAtDistance;
        float _distanceTimer;
        float _currentDistance;

        public CameraTrackingMode trackingMode = CameraTrackingMode.Follow;
        private float _endTime;
        private Transform spotTarget;
        private Action spotCallback;

        private void LateUpdate()
        {
            if (target == null)
                return;

            switch (trackingMode)
            {
                case CameraTrackingMode.Follow:
                    SyncDistance();
                    transform.position = target.position + (-target.forward * offset.z + target.up * offset.y).normalized * _currentDistance;
                    transform.LookAt(target.position + target.forward * lookAtDistance, Vector3.up);
                    break;

                case CameraTrackingMode.Spot:
                    if (Time.time > _endTime) {
                        if (spotCallback != null)
                            spotCallback();
                        ResumeToPlayer();
                        break;
                    }

                    transform.LookAt(spotTarget.position, Vector3.up);
                    break;
            }
        }

        public void ResumeToPlayer()
        {
            trackingMode = CameraTrackingMode.Follow;
            target = playerPlane;
            _distanceTimer = distanceLerpDuration;
        }

        void SyncDistance()
        {
            if (_distanceTimer > distanceLerpDuration)
                return;

            _distanceTimer += Time.deltaTime;
            var r = Mathf.Clamp01(_distanceTimer / distanceLerpDuration);
            _currentDistance = Mathf.Lerp(startDistance, endDistance, r);
        }

        public void ChangeTrackingMode(CameraTrackingMode mode, float duration, Transform spotTarget, Vector3 spotPos, Action callback = null)
        {
            trackingMode = mode;
            _endTime = Time.time + duration;
            transform.position = spotPos;
            this.spotTarget = spotTarget;
            spotCallback = callback;
        }
    }
}