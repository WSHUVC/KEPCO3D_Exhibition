using UnityEngine;
using Debug = WSH.Util.Debug;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WSH.UI
{
    public class UI_Panel_BottomButtons : PanelBase
    {
        public Button[] placeButtons;
        public override void Initialize()
        {
            base.Initialize();
            placeButtons = GetComponentsInChildren<Button>();
                
            panel_PlaceAndSensors = GetComponentsInChildren<UI_Panel_PlaceAndSensor>();
        }

        public override void Active()
        {
            base.Active();
            foreach(var p in panel_PlaceAndSensors)
            {
                p.Active();
            }
        }

        public UI_Panel_PlaceAndSensor[] panel_PlaceAndSensors;
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