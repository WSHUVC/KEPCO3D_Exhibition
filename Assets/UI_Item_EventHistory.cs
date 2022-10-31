using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Item_EventHistory : UIBase
    {
        Button button_Item;

        public override void Initialize()
        {
            base.Initialize();
            GetUIElement(name, out button_Item);
        }

        void OnClick_Item(UI_Item_EventHistory item)
        {
            Debug.Log($"OnClick_{name}");

        }
    }
}