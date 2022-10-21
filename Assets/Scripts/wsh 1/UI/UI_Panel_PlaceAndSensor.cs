using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Panel_PlaceAndSensor : PanelBase
    {
        UI_Panel_PlaceSensorList panel_PlaceSensorList;
        public Button button_Place;
        public override void Initialize()
        {
            base.Initialize();
            panel_PlaceSensorList = GetComponentInChildren<UI_Panel_PlaceSensorList>();
            TryGetUIElement("Button_Place", out button_Place);
            button_Place.onClick.AddListener(OnClick_Place);
        }
        public override void Active()
        {
            base.Active();
        }

        public override void Deactive()
        {
            panel_PlaceSensorList.PlayAnimation(true);
        }

        void OnClick_Place()
        {
            Debug.Log($"OnClick_Place:{name}");
            panel_PlaceSensorList.PlayAnimation();
        }
    }
}