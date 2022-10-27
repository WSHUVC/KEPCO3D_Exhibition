using UnityEngine;
using Debug = WSH.Util.Debug;
using UnityEngine.UI;
using System.Collections.Generic;
using WSH.Core.Manager;

namespace WSH.UI
{
    public class UI_Panel_BottomButtons : PanelBase
    {
        public UI_Panel_PlaceAndSensor[] panel_PlaceAndSensors;

        public override void Initialize()
        {
            panel_PlaceAndSensors = GetComponentsInChildren<UI_Panel_PlaceAndSensor>();
            foreach(var p in panel_PlaceAndSensors)
            {
                p.panel_Parent = this;
            }
        }

        public void OtherPanelRewind()
        {
            foreach(var panel in panel_PlaceAndSensors)
            {
                panel?.panel_PlaceSensorList?.currentSensor?.DeactiveOutline();
                panel.Deactive();
            }
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