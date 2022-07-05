using UnityEngine;

namespace Airplane
{
    public class PlaneTakeOffBehaviour : MonoBehaviour
    {
        public float disableInputDuration = 4;
        float _enableInputTimestamp;

        void Start()
        {
            _enableInputTimestamp = Time.time + disableInputDuration;
        }

        public bool InputDisabled
        {
            get { return Time.time < _enableInputTimestamp; }
        }
    }
}
