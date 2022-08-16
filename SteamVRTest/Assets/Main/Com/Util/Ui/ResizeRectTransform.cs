using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

namespace game
{
    public class ResizeRectTransform : MonoBehaviour
    {
        public RectTransform target;
        public RectTransform reference;
        public Text referenceOverrideText;
        public bool resizeHeight;
        public bool resizeWidth;
        public float heightOffset;
        public float widthOffset;
        public bool toResize = false;
        private bool _toResize2 = false;

        public void Update()
        {
            if (toResize)
            {
                toResize = false;
                Resize();
            }

            if (_toResize2)
            {
                toResize = true;
                _toResize2 = false;
            }
        }

        public void ResizeLater()
        {
            toResize = true;
            _toResize2 = true;
        }

        void Resize()
        {
            var size = target.sizeDelta;
            //Debug.Log(size);
            var sizeRef = reference.sizeDelta;
            if (referenceOverrideText != null)
            {
                //refText.autoSizeTextContainer = true;
                sizeRef.y = referenceOverrideText.renderedHeight;
                sizeRef.x = referenceOverrideText.renderedWidth;
            }

            if (resizeWidth)
            {
                size.x = sizeRef.x + widthOffset;
            }
            if (resizeHeight)
            {
                size.y = sizeRef.y + heightOffset;
            }
            //Debug.Log(size);
            target.sizeDelta = size;
        }
    }
}