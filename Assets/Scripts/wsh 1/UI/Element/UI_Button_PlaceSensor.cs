using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Button_PlaceSensor : UIBase
    {
        public int index;

        public Button button_PlaceSensor;

        public override void Initialize()
        {
            base.Initialize();
            //Debug.Log(gameObject.name);
            GetUIElement(gameObject.name, out button_PlaceSensor);
            button_PlaceSensor.onClick.AddListener(OnClick_MoveToSensor);
        }

        void OnClick_MoveToSensor()
        {
            Debug.Log($"{name}:OnClick_MoveToSensor:index={index}");
            GetCanvas<UI_Canvas_LeftMenu>().Active();
            GetCanvas<UI_Canvas_RightMenu>().Active();
            GetCanvas<UI_Canvas_RightMenu>().DeleteGraph();
            FindObjectOfType<CM_CameraManager>().ZoomintoSensor(mySensor);
        }
        public Tag_Sensor mySensor;
        public UI_Flag myFlag;
        public UI_Panel_PlaceSensorList panel;
        internal void SetPlace(UI_Panel_PlaceSensorList parent, Tag_Place place,int index)
        {
            panel = parent;
            this.index = index; 
            var sensors = place.tagMembers;
            var sensor = sensors.Where(s => s.index == index).First();
            mySensor = sensor as Tag_Sensor;
            myFlag = mySensor.myFlag.GetComponent<UI_Flag>();
            GetUIElement(gameObject.name, out button_PlaceSensor);
            button_PlaceSensor.onClick.AddListener(ActiveOutline);
        }

        internal void DeactiveOutline()
        {
            myFlag.DeactiveOutline();
        }

        void ActiveOutline()
        {
            panel.currentSensor = this;
            myFlag.ActiveOutline();
        }
    }
}
