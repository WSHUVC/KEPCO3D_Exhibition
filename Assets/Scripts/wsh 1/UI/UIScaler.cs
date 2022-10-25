using UnityEngine;

namespace WSH.UI
{
    public class UIScaler : MonoBehaviour
    {
        RectTransform rect;

        public Vector2 originSize;
        public Vector2 currentSize;
        Vector2 pivot;
        Vector2 anchorMin;
        Vector2 anchorMax;

        [HideInInspector]public float sizeRatio;
        public void SetScalePerRatio(float ratio)
        {
            rect = GetComponent<RectTransform>();
            //LoadPivot();
            sizeRatio = ratio;
            currentSize = originSize * sizeRatio;
            rect.sizeDelta = currentSize;
        }

        void SavePivot()
        {
            pivot = rect.pivot;
            anchorMin = rect.anchorMin;
            anchorMax = rect.anchorMax;
        }

        void LoadPivot()
        {
            rect.pivot = pivot;
            rect.anchorMin = anchorMin;
            rect.anchorMax = anchorMax;
        }

        public void OriginSizeChange()
        {
            rect = GetComponent<RectTransform>();
            //SavePivot();
            originSize = rect.sizeDelta;
            currentSize = rect.sizeDelta;
            sizeRatio = 1f;
        }
    }
}
