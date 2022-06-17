using System;
using UnityEngine;

namespace Mission {
    public class MissionPointBehaviour : MonoBehaviour {
        
        private Vector3 nextPosition;
        private Action onTriggerEnter;
        private bool isLastPosition;

        public bool GetIsLastPosition() {
            return isLastPosition;
        }
        
        public MissionPointBehaviour SetIsLastPosition(bool isLastPosition) {
            this.isLastPosition = isLastPosition;
            return this;
        }
        
        public MissionPointBehaviour SetPosition(Vector3 position) {
            transform.position = position;
            return this;
        }
        
        public MissionPointBehaviour SetNextPosition(Vector3 nextPosition) {
            this.nextPosition = nextPosition;
            return this;
        }

        public MissionPointBehaviour SetOnTriggerEnter(Action action) {
            onTriggerEnter += action;
            return this;
        }

        private void Start() {
        }

        private void OnTriggerEnter(Collider other) {
            if (onTriggerEnter != null) onTriggerEnter();
        }
    }
}