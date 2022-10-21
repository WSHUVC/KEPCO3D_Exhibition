using UnityEngine;
using Debug = WSH.Util.Debug;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WSH.UI
{
    public class UI_Panel_BottomButtons : PanelBase
    {
        UI_FlagButton[] placeButtons;
        public int length => placeButtons.Length;
        Dictionary<UI_FlagButton, UI_Flag> flagButton = new Dictionary<UI_FlagButton, UI_Flag>();
        public override void Initialize()
        {
            base.Initialize();
            flagButton.Clear();
            placeButtons = GetComponentsInChildren<UI_FlagButton>();
                
            for(int i = 0; i < placeButtons.Length; ++i)
            {
                placeButtons[i].AddListener(OnClick_PlaceButton);
            }
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

        UI_Panel_PlaceAndSensor[] panel_PlaceAndSensors;
        public override void Deactive()
        {
            base.Deactive();
            foreach(var p in panel_PlaceAndSensors)
            {
                p.Deactive();
            }
        }

        void OnClick_PlaceButton()
        {

        }
    }
}