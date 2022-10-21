using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Panel_PlaceSensorList : PanelBase
    {
        public UI_PlaceSensor[] button_Sensors;
        public override void Initialize()
        {
            base.Initialize();
            button_Sensors = GetComponentsInChildren<UI_PlaceSensor>();
        }
    }
}