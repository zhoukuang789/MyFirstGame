using System;
using UnityEngine;

namespace ProjectBase.Mono {
    /// <summary>
    /// mono的管理者，继承MonoBehaviour以提供Update方法和协程
    /// </summary>
    public class MonoController : MonoBehaviour {
        
        /// <summary>
        /// Update事件，Update()里每一帧都广播
        /// </summary>
        private event Action updateEvent;

        /// <summary>
        /// FixedUpdate事件，FiexdUpdate()里每一帧都广播
        /// </summary>
        private event Action fixedUpdateEvent;

        private void Start() {
            //此对象不可移除
            //从而方便别的对象找到该物体，从而获取脚本，从而添加方法
            DontDestroyOnLoad(gameObject);
        }

        private void Update() {
            // Debug.Log("MonoService Update====================================>");
            // 每一帧更新都向外广播update事件
            if (updateEvent != null) {
                updateEvent();
            }
        }

        private void FixedUpdate() {
            if (fixedUpdateEvent != null) {
                fixedUpdateEvent();
            }
        }

        /// <summary>
        /// 为外部提供订阅 updateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void AddUpdateEventListener(Action action) {
            updateEvent += action;
        }
        
        /// <summary>
        /// 为外部提供取消订阅 updateEvent 的方法
        /// </summary>
        /// <param name="function"></param>
        public void RemoveUpdateEventListener(Action action) {
            updateEvent -= action;
        }

        /// <summary>
        /// 为外部提供订阅 fixedUpdateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void AddFixedUpdateEventListener(Action action) {
            fixedUpdateEvent += action;
        }

        /// <summary>
        /// 为外部提供取消订阅 fixedUpdateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void RemoveFixedUpdateEventListener(Action action) {
            fixedUpdateEvent -= action;
        }
    }
}