using System;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Scripts {
    public class HintBehaviour : MonoBehaviour {

        // --------------field -----------------------------
        private CanvasGroup canvasGroup;
        private Text text;
        private float duration;
        private float endTime;
        private Action callback;


        // --------------mono method -----------------------------
        private void Awake() {
            canvasGroup = GetComponent<CanvasGroup>();
            text = GetComponentInChildren<Text>();
        }

        private void Start() {
            endTime = Time.time + duration;
        }

        private void Update() {
            if (Time.time > endTime) {
                Destroy(gameObject);
            }
        }

        private void OnDestroy() {
            if (callback != null)
                callback();
        }

        // --------------getter & setter -----------------------------
        public HintBehaviour SetText(string text) {
            this.text.text = text;
            return this;
        }

        public HintBehaviour SetDuration(float duration) {
            this.duration = duration;
            return this;
        }

        public HintBehaviour SetCallback(Action callback) {
            this.callback += callback;
            return this;
        }
        
    }
}