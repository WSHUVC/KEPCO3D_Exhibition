using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Item_EventHistory : UIBase
    {
        Button button_Item;
        public int index;

        public override void Initialize()
        {
            base.Initialize();
            GetUIElement(name, out button_Item);
            button_Item.onClick.AddListener(OnClick_Item);
        }

        void OnClick_Item()
        {
            Debug.Log($"OnClick_{name}");
            FindObjectOfType<QuadScript>().DrawGraph(this.index);
        }
    }
}