using System;
using Mission;
using ProjectBase.Input;
using Record;
using UnityEngine;

namespace Plane {
    public class PlayerController : MonoBehaviour {
        private PlaneMoveBehaviour planeBehaviour;

        private KeyItem wKey;

        private KeyItem sKey;

        private KeyItem aKey;

        private KeyItem dKey;

        private KeyItem spaceKey;

        private void Start() {
            planeBehaviour = GetComponent<PlaneMoveBehaviour>();


           
        }

        private void OnEnable() {
            // 开启输入
            InputService.GetInstance().StartInput();

            wKey = new KeyItem()
                .SetKeyCode(KeyCode.W)
                .SetKeyName("飞机加速")
                .SetOnKeyInput(OnWKeyInput);
            sKey = new KeyItem()
                .SetKeyCode(KeyCode.S)
                .SetKeyName("飞机减速")
                .SetOnKeyInput(OnSKeyInput);
            aKey = new KeyItem()
                .SetKeyCode(KeyCode.A)
                .SetKeyName("向左偏航")
                .SetOnKeyInput(OnAKeyInput);
            dKey = new KeyItem()
                .SetKeyCode(KeyCode.D)
                .SetKeyName("向右偏航")
                .SetOnKeyInput(OnDKeyInput);

            spaceKey = new KeyItem()
                .SetKeyCode(KeyCode.Space)
                .SetKeyName("飞机向上俯仰")
                .SetOnKeyInput(OnSpaceKeyInout);


            InputService.GetInstance()
                .RegisterKey(wKey, InputService.InputRegisterMethod.InFixedUpdate)
                .RegisterKey(sKey, InputService.InputRegisterMethod.InFixedUpdate)
                .RegisterKey(aKey)
                .RegisterKey(dKey)
                .RegisterKey(spaceKey)
                .RegisterMouseMove(OnMouseMove);

            InputService.GetInstance()
                .RegisterKey(new KeyItem().SetKeyCode(KeyCode.K).SetKeyName("Test").SetOnKeyDown(() => {
                    GameObject.Find("Bomber").GetComponent<PlaneHealth>().Die();
                }));
        }

        /// <summary>
        /// 加速按键
        /// </summary>
        private void OnWKeyInput() {
            PlaneControllerService.GetInstance().SetPlane(planeBehaviour).AddTrust(wKey.GetVolume());
        }

        /// <summary>
        /// 减速按键
        /// </summary>
        private void OnSKeyInput() {
            PlaneControllerService.GetInstance().SetPlane(planeBehaviour).ReduceTrust(sKey.GetVolume());
        }

        private void OnAKeyInput() {
            PlaneControllerService.GetInstance().SetPlane(planeBehaviour).YawLeft(aKey.GetVolume());
        }

        private void OnDKeyInput() {
            PlaneControllerService.GetInstance().SetPlane(planeBehaviour).YawRight(dKey.GetVolume());
        }

        private void OnSpaceKeyInout() {
            PlaneControllerService.GetInstance().SetPlane(planeBehaviour).DoPitch(spaceKey.GetVolume());
        }

        private void OnMouseMove(Vector2 mouseAxis) {
            if (mouseAxis.x != 0 || mouseAxis.y != 0) {
                PlaneControllerService.GetInstance().SetPlane(planeBehaviour).DoRoll(Mathf.Clamp(mouseAxis.x, -1, 1));
                PlaneControllerService.GetInstance().SetPlane(planeBehaviour).DoPitch(Mathf.Clamp(mouseAxis.y, -1, 1));
            }
        }
    }
}