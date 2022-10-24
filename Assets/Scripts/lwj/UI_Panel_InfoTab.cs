using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WSH.Core.Manager;
using Debug = WSH.Util.Debug;

namespace WSH.UI
{
    public class UI_Panel_InfoTab : PanelBase
    {
        Button button_FacilitiesInfo;
        public override void Initialize()
        {
            if (GetUIElement("Button_FacilitiesInfo", out button_FacilitiesInfo))
                button_FacilitiesInfo.onClick.AddListener(OnClick_Info);
        }

        void OnClick_Info()
        {
            GetCanvas<UI_Canvas_Tabs>().Active();
        }
    }
}

