using Airplane.Movement;
using Airplane.Weapon;
using ProjectBase.Input;
using UnityEngine;

namespace Airplane
{
    public class PlayerController : MonoBehaviour
    {
        private PlaneBehaviour plane;
        private PlaneTakeOffBehaviour takeOff;

        private KeyItem wKey;

        private KeyItem sKey;

        private KeyItem aKey;

        private KeyItem dKey;

        private KeyItem spaceKey;

        private KeyItem mouse0Key;

        private void Awake()
        {
            plane = GetComponent<PlaneBehaviour>();
            takeOff = GetComponent<PlaneTakeOffBehaviour>();
        }

        private void OnEnable()
        {
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
                .SetOnKeyInput(OnMouse0KeyInput);


            InputService.GetInstance()
                .RegisterKey(wKey, InputService.InputRegisterMethod.InFixedUpdate)
                .RegisterKey(sKey, InputService.InputRegisterMethod.InFixedUpdate)
                .RegisterKey(aKey)
                .RegisterKey(dKey)
                .RegisterKey(spaceKey)
                .RegisterKey(mouse0Key)
                .RegisterMouseMove(OnMouseMove);
        }

        private void OnDisable()
        {
            InputService.GetInstance().CloseInput();
        }

        bool TakeOffInputDisabled
        {
            get { return takeOff != null && takeOff.enabled && takeOff.InputDisabled; }
        }

        /// <summary>
        /// 加速按键
        /// </summary>
        private void OnWKeyInput()
        {
            if (TakeOffInputDisabled)
                return;
            PlaneMovementControllerService.GetInstance().SetPlane(plane).AddTrust(wKey.GetVolume());
        }

        /// <summary>
        /// 减速按键
        /// </summary>
        private void OnSKeyInput()
        {
            if (TakeOffInputDisabled)
                return;
            PlaneMovementControllerService.GetInstance().SetPlane(plane).ReduceTrust(sKey.GetVolume());
        }

        private void OnAKeyInput()
        {
            if (TakeOffInputDisabled)
                return;
            PlaneMovementControllerService.GetInstance().SetPlane(plane).DoYaw(aKey.GetVolume());
        }

        private void OnDKeyInput()
        {
            if (TakeOffInputDisabled)
                return;
            PlaneMovementControllerService.GetInstance().SetPlane(plane).DoYaw(-aKey.GetVolume());
        }

        private void OnSpaceKeyInout()
        {
            if (TakeOffInputDisabled)
                return;
            PlaneMovementControllerService.GetInstance().SetPlane(plane).DoPitch(spaceKey.GetVolume());
        }

        private void OnMouseMove(Vector2 mouseAxis)
        {
            if (TakeOffInputDisabled)
                return;
            if (mouseAxis.x != 0 || mouseAxis.y != 0)
            {
                PlaneMovementControllerService.GetInstance().SetPlane(plane)
                    .DoRoll(Mathf.Clamp(mouseAxis.x, -1, 1));
                PlaneMovementControllerService.GetInstance().SetPlane(plane)
                    .DoPitch(Mathf.Clamp(mouseAxis.y, -1, 1));
            }
        }

        private void OnMouse0KeyInput()
        {
            if (TakeOffInputDisabled)
                return;
            PlaneWeaponControllerService.GetInstance().SetPlane(plane).Fire();
        }
    }
}