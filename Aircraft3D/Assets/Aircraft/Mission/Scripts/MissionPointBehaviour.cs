using System;
using UnityEngine;

namespace Mission {
    public class MissionPointBehaviour : MonoBehaviour {
        
        private Vector3 nextPosition;
        private Action<Collider> onTriggerEnter;
        private bool isLastPosition;
        private bool isAutoDestroy;

        public bool GetIsLastPosition() {
            return isLastPosition;
        }
        
        public MissionPointBehaviour SetIsLastPosition(bool isLastPosition) {
            this.isLastPosition = isLastPosition;
            return this;
        }
        
        public MissionPointBehaviour SetIsAutoDestroy(bool isAutoDestroy) {
            this.isAutoDestroy = isAutoDestroy;
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

        public MissionPointBehaviour SetOnTriggerEnter(Action<Collider> action) {
            onTriggerEnter += action;
            return this;
        }

        private void Start() {
        }

        private void OnTriggerEnter(Collider other) {
            if (onTriggerEnter != null) onTriggerEnter(other);
            if (isAutoDestroy) {
                Destroy(gameObject);
            }
        }
    }
}