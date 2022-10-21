using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Panel_PlaceSensorList : PanelBase
    {
        public Button[] button_Sensors;
        public override void Initialize()
        {
            base.Initialize();
            button_Sensors = uiElements
                .Where(ui => ui is Button)
                .Select(i=>i as Button)
                .ToArray();
        }
    }
}