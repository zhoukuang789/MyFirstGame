using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

namespace ProjectBase.Input {
    
    /// <summary>
    /// 键盘按键实体类
    /// </summary>
    public class KeyItem {
        
        private KeyCode keyCode;
        
        private string keyName;
        
        private Action onKeyDown;

        private Action onKeyInput;

        private Action onKeyUp;

        /// <summary>
        /// 按键持续时间，从按下开始算，抬起时结束
        /// </summary>
        private float keyInputTime;
        private float lastTime;

        /// <summary>
        /// 杆量
        /// </summary>
        private float volume;

        // 最大杆量时间
        private float maxVolumeTime = 2f;
        
        public KeyItem() {
            onKeyDown += () => {
                keyInputTime = 0f;
                lastTime = Time.time;
            };
            onKeyInput += () => {
                keyInputTime += Time.time - lastTime;
                lastTime = Time.time;
            };
        }

        public float GetVolume() {
            volume = (1f / maxVolumeTime) * keyInputTime;
            volume = Mathf.Clamp01(volume);
            return volume;
        }

        public float GetKeyInputTime() {
            return keyInputTime;
        }

        public KeyCode GetKeyCode() {
            return keyCode;
        }
        public KeyItem SetKeyCode(KeyCode keyCode) {
            this.keyCode = keyCode;
            return this;
        }

        public string GetKeyName() {
            return keyName;
        }
        public KeyItem SetKeyName(string keyName) {
            this.keyName = keyName;
            return this;
        }

        public Action GetOnKeyDown() {
            return onKeyDown;
        }
        public KeyItem SetOnKeyDown(Action onKeyDown) {
            this.onKeyDown += onKeyDown;
            return this;
        }

        public Action GetOnKeyInput() {
            return onKeyInput;
        }
        public KeyItem SetOnKeyInput(Action onKeyInput) {
            this.onKeyInput += onKeyInput;
            return this;
        }

        public Action GetOnKeyUp() {
            return onKeyUp;
        }
        public KeyItem SetOnKeyUp(Action onKeyUp) {
            this.onKeyUp += onKeyUp;
            return this;
        }

    }
    
}