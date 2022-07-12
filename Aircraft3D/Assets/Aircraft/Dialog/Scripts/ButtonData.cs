using System;
using UnityEngine.UI;

namespace Dialog.Scripts {
    public class ButtonData {
        private string text;
        private Action onClick;

        public ButtonData() {
            
        }
        
        public ButtonData(string text, Action onClick) {
            this.text = text;
            this.onClick += onClick;
        }
        
        public string GetText() {
            return text;
        }

        public ButtonData SetText(string text) {
            this.text = text;
            return this;
        }

        public Action GetOnClick() {
            return onClick;
        }

        public ButtonData SetOnClick(Action onClick) {
            this.onClick += onClick;
            return this;
        }
    }
}