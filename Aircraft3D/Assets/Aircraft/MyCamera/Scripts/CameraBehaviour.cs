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

        private void LateUpdate()
        {
            if (target == null)
                return;

            SyncDistance();
            transform.position = target.position + (-target.forward * offset.z + target.up * offset.y).normalized * _currentDistance;
            transform.LookAt(target.position + target.forward * lookAtDistance, Vector3.up);
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
    }
}