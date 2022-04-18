using UnityEngine;

namespace com
{
    public class ReferenceService : MonoBehaviour
    {
        public static ReferenceService instance { get; private set; }

        public PlaneBehaviour playerPlane;

        private void Awake()
        {
            instance = this;
        }
    }
}