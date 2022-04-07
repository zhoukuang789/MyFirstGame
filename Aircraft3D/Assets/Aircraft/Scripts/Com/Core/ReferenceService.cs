using UnityEngine;

namespace com
{
    public class ReferenceService : MonoBehaviour
    {
        public static ReferenceService instance { get; private set; }

        public PlayerPlaneController playerPlane;

        private void Awake()
        {
            instance = this;
        }
    }
}