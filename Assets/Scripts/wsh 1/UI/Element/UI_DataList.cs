using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_DataList : UIBase
    {
        public UI_Item_EventHistory prefab_Item;
        ScrollRect scrollRect;
        RectTransform parent_Item;
        private void Awake()
        {
            scrollRect = GetComponentInChildren<ScrollRect>();
            parent_Item = scrollRect.content;
        }
        List<UI_Item_EventHistory> items = new List<UI_Item_EventHistory>();
        public void Refresh(UI_Button_PlaceSensor[] sensors)
        {
            if ( items.Count > 0)
            {
                foreach(UI_Item_EventHistory tmp in items)
                {
                    tmp.Deactive();
                    Destroy(tmp.gameObject);
                }

                items.Clear();
            }

            items.AddRange(Add(3));

        }

        UI_Item_EventHistory[] Add(int count = 1)
        {
            UI_Item_EventHistory[] result = new UI_Item_EventHistory[count];
            for (int i = 0; i < count; ++i)
            {
                var item = Instantiate(prefab_Item);
                item.transform.SetParent(parent_Item);
                item.GetComponent<UI_Item_EventHistory>().index = i;
                item.GetComponent<UI_Item_EventHistory>().Initialize();
                result[i] = item;
            }
            return result;
        }
    }
}