using System;
using System.Collections.Generic;
using ProjectBase.Mono;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace ProjectBase.Input {
    
    /// <summary>
    /// 提供Input输入服务的类。
    /// 使用方法：1.首先需要Start()开启输入检测；
    /// 2.创建键盘的KeyItem对象，然后注册；
    /// 3.注册鼠标的MouseMove对象
    /// </summary>
    public class InputService : Singletonable<InputService> {

        /// <summary>
        /// 输入检测是否开启
        /// </summary>
        private bool isStart;

        /// <summary>
        /// 在 update 里使用的按键
        /// </summary>
        private List<KeyItem> keyItemInUpdateList;

        /// <summary>
        /// 在 fixedUpdate 里使用的按键
        /// </summary>
        private List<KeyItem> keyItemInFixedUpdateList;

        /// <summary>
        /// 是否开启鼠标输入
        /// </summary>
        private bool isMouseMoveInUpdate;
        private bool isMouseMoveInFixedUpdate;
        private Action<Vector2> onMouseMove;
        

        /// <summary>
        /// 构造方法
        /// </summary>
        public InputService() {
            // 初始化List
            keyItemInUpdateList = new List<KeyItem>();
            keyItemInFixedUpdateList = new List<KeyItem>();
        }

        /// <summary>
        /// 提供给外部使用的开启输入检测的方法
        /// </summary>
        public void StartInput() {
            // 用InputUpdate函数订阅Mono的Update事件
            MonoService.GetInstance().AddUpdateEventListener(InputUpdate);
            MonoService.GetInstance().AddFixedUpdateEventListener(InputFixedUpdate);
            isStart = true;
        }

        /// <summary>
        /// 提供给外部使用的关闭输入检测的方法
        /// </summary>
        public void CloseInput() {
            MonoService.GetInstance().RemoveUpdateEventListener(InputUpdate);
            MonoService.GetInstance().RemoveFixedUpdateEventListener(InputFixedUpdate);
            keyItemInUpdateList.Clear();
            keyItemInFixedUpdateList.Clear();
            onMouseMove = null;
            isStart = false;
        }

        /// <summary>
        /// 按键注册方式
        /// </summary>
        public enum InputRegisterMethod {
            InUpdate,
            InFixedUpdate
        }
        /// <summary>
        /// 注册一个按键
        /// </summary>
        /// <param name="keyItem"></param>
        /// <param name="keyRegisterMethod">InUpdate or InFixedUpdate，默认是InUpdate</param>
        public InputService RegisterKey(KeyItem keyItem, InputRegisterMethod keyRegisterMethod = InputRegisterMethod.InUpdate) {
            switch (keyRegisterMethod) {
                case InputRegisterMethod.InUpdate:
                    keyItemInUpdateList.Add(keyItem);
                    return this;
                case InputRegisterMethod.InFixedUpdate:
                    keyItemInFixedUpdateList.Add(keyItem);
                    return this;
                default:
                    keyItemInUpdateList.Add(keyItem);
                    return this;
            }
        }

        /// <summary>
        /// 注册鼠标移动
        /// </summary>
        /// <param name="action"></param>
        /// <param name="mouseRegisterMethod"></param>
        public InputService RegisterMouseMove(Action<Vector2> action, InputRegisterMethod mouseRegisterMethod = InputRegisterMethod.InUpdate) {
            switch (mouseRegisterMethod) {
                case InputRegisterMethod.InUpdate:
                    isMouseMoveInUpdate = true;
                    isMouseMoveInFixedUpdate = false;
                    onMouseMove += action;
                    return this;
                case InputRegisterMethod.InFixedUpdate:
                    isMouseMoveInFixedUpdate = true;
                    isMouseMoveInUpdate = false;
                    onMouseMove += action;
                    return this;
                default:
                    isMouseMoveInUpdate = true;
                    isMouseMoveInFixedUpdate = false;
                    onMouseMove += action;
                    return this;
            }
        }
        
        /// <summary>
        /// 每一次更新执行
        /// </summary>
        private void InputUpdate() {
            // 如果没有开启输入检测，就不去检测
            if (!isStart) {
                return;
            }
            
            // 检测键盘按键输入
            foreach (KeyItem keyItem in keyItemInUpdateList) {
                CheckKeyInput(keyItem);
            }

            // 检测鼠标移动
            if (isMouseMoveInUpdate) {
                CheckMouseMove();
            }
        }

        private void InputFixedUpdate() {
            // 如果没有开启输入检测，就不去检测
            if (!isStart) {
                return;
            }

            // 检测键盘按键输入
            foreach (KeyItem keyItem in keyItemInFixedUpdateList) {
                CheckKeyInput(keyItem);
            }
            
            // 检测鼠标移动
            if (isMouseMoveInFixedUpdate) {
                CheckMouseMove();
            }
        }

        private void CheckKeyInput(KeyItem keyItem) {

            // 按键按下
            if (UnityEngine.Input.GetKeyDown(keyItem.GetKeyCode())) {
                if (keyItem.GetOnKeyDown() != null) {
                    keyItem.GetOnKeyDown()();
                }
            }
            
            
            // 按键持续输入
            if (UnityEngine.Input.GetKey(keyItem.GetKeyCode())) {
                if (keyItem.GetOnKeyInput() != null) {
                    keyItem.GetOnKeyInput()();
                }
            }
            
            // 按键抬起时
            if (UnityEngine.Input.GetKeyUp(keyItem.GetKeyCode())) {
                if (keyItem.GetOnKeyUp() != null) {
                    keyItem.GetOnKeyUp()();
                }
            }
        }
        
        
        /// <summary>
        /// 检测鼠标移动
        /// </summary>
        private void CheckMouseMove() {
            float mouseXAxis = UnityEngine.Input.GetAxis("Mouse X");
            float mouseYAxis = UnityEngine.Input.GetAxis("Mouse Y");
            Vector2 mouseAxis = new Vector2(mouseXAxis, mouseYAxis);
            // 如果鼠标移动了，广播鼠标移动的事件
            if (mouseXAxis != 0 || mouseYAxis != 0) {
                onMouseMove(mouseAxis);
            }
        }

        
    }
}