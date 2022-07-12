using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Dialog.Scripts {
    public class ButtonBehaviour : MonoBehaviour {

        private Action onClick;

        public void OnClick() {
            if (onClick != null)
                onClick();
        }
        
        public ButtonBehaviour SetText(string text) {
            TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = text;
            return this;
        }

        public ButtonBehaviour AddOnClick(Action onClick) {
            this.onClick += onClick;
            return this;
        }
        
    }
}