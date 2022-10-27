using UnityEngine;
using Debug = WSH.Util.Debug;
using UnityEngine.UI;
using System.Collections.Generic;
using WSH.Core.Manager;

namespace WSH.UI
{
    public class UI_Panel_BottomButtons : PanelBase
    {
        public Button[] placeButtons;
        public UI_Panel_PlaceAndSensor[] panel_PlaceAndSensors;

        public override void Initialize()
        {
            placeButtons = GetComponentsInChildren<Button>();
                
            panel_PlaceAndSensors = GetComponentsInChildren<UI_Panel_PlaceAndSensor>();
            foreach(var p in panel_PlaceAndSensors)
            {
                p.panel_Parent = this;
            }
        }

        UI_Panel_PlaceAndSensor prev;
        public void OtherPanelRewind(UI_Panel_PlaceAndSensor currentPanel)
        {
            if (prev != null)
                prev.Deactive();
            prev = currentPanel;
            if (prev.panel_PlaceSensorList.currentSensor != null)
                prev.panel_PlaceSensorList.currentSensor.DeactiveOutline();
            GetCanvas<UI_Canvas_LeftMenu>().Deactive();
            GetCanvas<UI_Canvas_RightMenu>().Deactive();
        }

        public override void Deactive()
        {
            base.Deactive();
            foreach(var p in panel_PlaceAndSensors)
            {
                p.Deactive();
            }
        }
    }
}