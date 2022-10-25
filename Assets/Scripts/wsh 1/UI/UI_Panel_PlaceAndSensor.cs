using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using WSH.Core;
using Debug = WSH.Util.Debug;

namespace WSH.UI
{
    public class UI_Panel_PlaceAndSensor : PanelBase
    {
        public UI_Panel_PlaceSensorList panel_PlaceSensorList;
        public int index;
        public Button button_Place;
        internal UI_Panel_BottomButtons panel_Parent;

        public override void Initialize()
        {
            panel_PlaceSensorList = GetComponentInChildren<UI_Panel_PlaceSensorList>();
            GetUIElement("Button_Place", out button_Place);
            button_Place.onClick.AddListener(OnClick_Place);
        }

        public override void Deactive()
        {
            panel_PlaceSensorList.RewindAnimation();
        }

        void OnClick_Place()
        {
            Debug.Log($"OnClick_Place:{name}");
            panel_Parent.OtherPanelRewind(this);
            panel_PlaceSensorList.PlayAnimation();
            FindObjectOfType<Managers>().ActiveSensorFlag();
            FindObjectOfType<CM_CameraManager>().MoveTo(index);
        }
    }
}