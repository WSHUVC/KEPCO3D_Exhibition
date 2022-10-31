using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WSH.Core;
using Debug = WSH.Util.Debug;

namespace WSH.UI
{
    public class UI_Panel_PlaceAndSensor : PanelBase
    {
        CM_CameraManager cmManager;
        public UI_Panel_PlaceSensorList panel_PlaceSensorList;
        public int index;
        public Button button_Place;
        internal UI_Panel_BottomButtons panel_Parent;

        EventSystem es;
        public override void Initialize()
        {
            es = FindObjectOfType<EventSystem>();
            panel_PlaceSensorList = GetComponentInChildren<UI_Panel_PlaceSensorList>();
            GetUIElement("Button_Place", out button_Place);
            button_Place.onClick.AddListener(OnClick_Place);
            cmManager = FindObjectOfType<CM_CameraManager>();
            cmManager.cameraMoveEndEvent += CameraEvent;
            ui_DataList = FindObjectOfType<UI_DataList>();
        }

        public override void Deactive()
        {
            panel_PlaceSensorList.RewindAnimation();
        }

        void OnClick_Place()
        {
            //FindObjectOfType<Managers>().ActiveSensorFlag();
            Debug.Log($"OnClick_Place:{name}");
            if (cmManager.MoveTo(index, button_Place))
            {
                panel_Parent.OtherPanelRewind();
                es.enabled = false;
            }
        }
        UI_DataList ui_DataList;
        void CameraEvent(Button button)
        {
            if (button != button_Place)
                return;
            var sensors = panel_PlaceSensorList.button_PlaceSensors;
            ui_DataList.Refresh(sensors);
            panel_PlaceSensorList.PlayAnimation();
            es.enabled = true;
        }
    }
}