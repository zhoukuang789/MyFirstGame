using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog.Scripts
{
    public class MenuBehaviour : MonoBehaviour
    {

        public Transform buttons;

        public Image vignette;
        private int buttonCount;

        private void Awake()
        {
            buttonCount = 0;
        }

        public MenuBehaviour SetText(string text)
        {
            TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = text;
            return this;
        }

        public void SetVignette(bool b)
        {
            vignette.enabled = b;
        }

        /// <summary>
        /// 添加菜单按钮
        /// </summary>
        public void AddButton(string text, Action onClick)
        {
            buttonCount++;
            if (buttonCount > 3) return;
            Transform buttonTransform = buttons.transform.GetChild(buttonCount - 1);
            GameObject menuButtonPrefab = Resources.Load<GameObject>("Prefabs/MenuButton");
            GameObject menuButton = Instantiate(menuButtonPrefab, buttonTransform.position, buttonTransform.rotation, buttons.transform);
            ButtonBehaviour buttonBehaviour = menuButton.GetComponent<ButtonBehaviour>();
            buttonBehaviour.SetText(text).AddOnClick(onClick);
            menuButton.SetActive(true);
        }

        public void Show()
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(true);
            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.DOFade(1, 2).SetDelay(0.25f);
            cg.blocksRaycasts = true;
        }
    }
}