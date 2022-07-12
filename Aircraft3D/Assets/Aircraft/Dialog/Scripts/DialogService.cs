using System;
using System.Collections.Generic;
using DG.Tweening;
using ProjectBase.SingletonBase;
using UnityEngine;

namespace Dialog.Scripts {
    public class DialogService : Singletonable<DialogService> {
        
        /// <summary>
        /// 弹出Hint框
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        /// <param name="callback"></param>
        public void Hint(string text, float duration = 6f, Action callback = null) {
            GameObject hintPrefab = Resources.Load<GameObject>("Prefabs/Hint");
            GameObject canvas = GameObject.Find("Canvas");
            GameObject hint = GameObject.Instantiate(hintPrefab, canvas.transform);
            HintBehaviour hintBehaviour = hint.GetComponent<HintBehaviour>();
            hintBehaviour.SetText(text).SetDuration(duration).SetCallback(callback);
            hint.SetActive(true);
            CanvasGroup cg = hint.GetComponent<CanvasGroup>();
            cg.DOFade(1, 2).SetDelay(0.25f);
            cg.blocksRaycasts = true;
        }

        /// <summary>
        /// 显示菜单
        /// </summary>
        public void ShowMenu(string text, List<ButtonData> buttonList) {
            GameObject menuPrefab = Resources.Load<GameObject>("Prefabs/Menu");
            GameObject canvas = GameObject.Find("Canvas");
            GameObject menu = GameObject.Instantiate(menuPrefab, canvas.transform);
            MenuBehaviour menuBehaviour = menu.GetComponent<MenuBehaviour>();
            menuBehaviour.SetText(text);
            foreach (ButtonData button in buttonList) {
                menuBehaviour.AddButton(button.GetText(), button.GetOnClick());
            }
            menu.SetActive(true);
            CanvasGroup cg = menu.GetComponent<CanvasGroup>();
            cg.DOFade(1, 2).SetDelay(0.25f);
            cg.blocksRaycasts = true;
        }

        
        
    }
}