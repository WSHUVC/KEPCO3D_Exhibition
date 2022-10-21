using UnityEngine;

namespace WSH.UI
{
    public class UIScaler : MonoBehaviour
    {
        RectTransform rect;

        public Vector2 originSize;
        public Vector2 currentSize;

        [HideInInspector]public float sizeRatio;
        public void SetScalePerRatio(float ratio)
        {
            rect = GetComponent<RectTransform>();
            sizeRatio = ratio;
            currentSize = originSize * sizeRatio;
            rect.sizeDelta = currentSize;
        }

        public void OriginSizeChange()
        {
            rect = GetComponent<RectTransform>();
            originSize = rect.sizeDelta;
            currentSize = rect.sizeDelta;
            sizeRatio = 1f;
        }
    }
}
