using System;
using System.Collections;
using System.ComponentModel;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace ProjectBase.Mono {
    
    /// <summary>
    /// 这个类用于给非MonoBehaviour类提供Update方法和协程
    /// </summary>
    public class MonoService : Singletonable<MonoService> {

        /// <summary>
        /// 创建一个用于挂载MonoController脚本的gameObject，并挂载MonoController脚本
        /// </summary>
        private readonly MonoController monoController;

        public MonoService() {
            // 创建一个用于挂载MonoController脚本的gameObject，并挂载MonoController脚本
            monoController = new GameObject("MonoService").AddComponent<MonoController>();
        }
        
        /// <summary>
        /// 为外部提供订阅 updateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void AddUpdateEventListener(Action action) {
            monoController.AddUpdateEventListener(action);
        }
        
        /// <summary>
        /// 为外部提供取消订阅 updateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void RemoveUpdateEventListener(Action action) {
            monoController.RemoveUpdateEventListener(action);
        }

        /// <summary>
        /// 为外部提供订阅 fixedUpdateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void AddFixedUpdateEventListener(Action action) {
            monoController.AddFixedUpdateEventListener(action);
        }

        /// <summary>
        /// 为外部提供取消订阅 fixedUpdateEvent 的方法
        /// </summary>
        /// <param name="action"></param>
        public void RemoveFixedUpdateEventListener(Action action) {
            monoController.RemoveFixedUpdateEventListener(action);
        }
        
        /// <summary>
        /// 提供协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public Coroutine StartCoroutine(IEnumerator routine) {
            return monoController.StartCoroutine(routine);
        }
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) {
            return monoController.StartCoroutine(methodName,value);
        }
        public Coroutine StartCoroutine(string methodName) {
            return monoController.StartCoroutine(methodName);
        }
    }
}