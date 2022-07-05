using UnityEngine;

namespace MyCamera
{
    public class CameraBehaviour : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float distance;

        private void LateUpdate()
        {
            if (target == null)
                return;

            transform.position = target.position + (-target.forward * offset.z + target.up * offset.y);
            transform.LookAt(target.position + target.forward * distance, Vector3.up);
        }
    }
}