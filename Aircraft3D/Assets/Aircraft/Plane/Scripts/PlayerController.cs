using System;
using Mission;
using Plane.Movement;
using Plane.Weapon;
using ProjectBase.Input;
using Record;
using UnityEngine;

namespace Plane {
    public class PlayerController : MonoBehaviour {
        
        private PlaneMovementBehaviour planeMovement;

        private PlaneWeaponBehaviour planeWeapon;

        private KeyItem wKey;

        private KeyItem sKey;

        private KeyItem aKey;

        private KeyItem dKey;

        private KeyItem spaceKey;
        
        private KeyItem mouse0Key;

        private void Awake() {
            planeMovement = GetComponent<PlaneMovementBehaviour>();
            planeWeapon = GetComponent<PlaneWeaponBehaviour>();
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

            mouse0Key = new KeyItem()
                .SetKeyCode(KeyCode.Mouse0)
                .SetKeyName("开火")
                .SetOnKeyDown(OnMouse0KeyDown)
                .SetOnKeyInput(OnMouse0KeyInput)
                .SetOnKeyUp(OnMouse0KeyUp);


            InputService.GetInstance()
                .RegisterKey(wKey, InputService.InputRegisterMethod.InFixedUpdate)
                .RegisterKey(sKey, InputService.InputRegisterMethod.InFixedUpdate)
                .RegisterKey(aKey)
                .RegisterKey(dKey)
                .RegisterKey(spaceKey)
                .RegisterKey(mouse0Key)
                .RegisterMouseMove(OnMouseMove);
        }

        private void OnDisable() {
            InputService.GetInstance().CloseInput();
        }

        /// <summary>
        /// 加速按键
        /// </summary>
        private void OnWKeyInput() {
            PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).AddTrust(wKey.GetVolume());
        }

        /// <summary>
        /// 减速按键
        /// </summary>
        private void OnSKeyInput() {
            PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).ReduceTrust(sKey.GetVolume());
        }

        private void OnAKeyInput() {
            PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).DoYaw(aKey.GetVolume());
        }

        private void OnDKeyInput() {
            PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).DoYaw(-aKey.GetVolume());
        }

        private void OnSpaceKeyInout() {
            PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).DoPitch(spaceKey.GetVolume());
        }

        private void OnMouseMove(Vector2 mouseAxis) {
            if (mouseAxis.x != 0 || mouseAxis.y != 0) {
                PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).DoRoll(Mathf.Clamp(mouseAxis.x, -1, 1));
                PlaneMovementControllerService.GetInstance().SetPlaneMovement(planeMovement).DoPitch(Mathf.Clamp(mouseAxis.y, -1, 1));
            }
        }


        private void OnMouse0KeyDown() {
            
        }
        
        private void OnMouse0KeyInput() {
            PlaneWeaponControllerService.GetInstance().SetPlaneWeapon(planeWeapon).Fire();
        }

        private void OnMouse0KeyUp() {
            
        }
    }
}