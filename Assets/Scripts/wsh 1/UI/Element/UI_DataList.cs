using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_DataList : MonoBehaviour
    {
        public GameObject prefab_Item;
        ScrollRect scrollRect;
        RectTransform parent_Item;
        private void Awake()
        {
            scrollRect = GetComponentInChildren<ScrollRect>();
            parent_Item = scrollRect.content;
        }
        public void Add(int count = 1)
        {
            for (int i = 0; i < count; ++i)
            {
                var item = Instantiate(prefab_Item);
                item.transform.SetParent(parent_Item);
            }
        }
    }
}