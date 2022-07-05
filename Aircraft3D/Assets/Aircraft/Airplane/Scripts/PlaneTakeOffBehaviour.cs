using UnityEngine;
using Airplane.Movement;

namespace Airplane
{
    public class PlaneTakeOffBehaviour : MonoBehaviour
    {
        public float disableInputDuration = 4;

        PlaneBehaviour plane;

        float _enableInputTimestamp;

        void Start()
        {
            plane = GetComponent<PlaneBehaviour>();
            _enableInputTimestamp = Time.time + disableInputDuration;
        }

        private void FixedUpdate()
        {
            if (InputDisabled)
            {
                PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(1);
            }
        }

        public bool InputDisabled
        {
            get { return Time.time < _enableInputTimestamp; }
        }
    }
}
