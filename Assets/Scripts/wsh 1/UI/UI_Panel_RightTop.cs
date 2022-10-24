using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WSH.Core;
using WSH.Core.Manager;
using Debug = WSH.Util.Debug;
namespace WSH.UI
{
    public class UI_Panel_RightTop : PanelBase
    {
        TextMeshProUGUI text_Date;
        Button button_Home;
        Button button_SceneChange;
        Button button_SimulationMode;
        Button button_Sensor;

        public override void Initialize()
        {
            GetUIElement("Button_Home", out button_Home);
            GetUIElement("Button_SceneChange", out button_SceneChange);
            GetUIElement("Button_SimulationMode", out button_SimulationMode);
            GetUIElement("Button_Sensor", out button_Sensor);
            GetUIElement("Text_Date", out text_Date);

            button_Home.onClick.AddListener(OnClick_Home);
            button_SceneChange.onClick.AddListener(OnClick_SceneChange);
            button_SimulationMode.onClick.AddListener(OnClick_SimulationMode);
            button_Sensor.onClick.AddListener(OnClick_Sensor);
            //GetPanel(out panel_BottomButtons); 
                //UIManager.instance.GetCanvas<UI_Canvas_Bottom>().GetPanel<UI_Panel_BottomButtons>();
        }

        void OnClick_Home()
        {
            Debug.Log($"{button_Home}:OnClick_Home");
            GetCanvas<UI_Canvas_Idle>().IdleOn();
        }

        void OnClick_SceneChange()
        {
            Debug.Log($"{button_SceneChange}:OnClick_SceneChange");
        }

        void OnClick_SimulationMode()
        {
            Debug.Log($"{button_SceneChange}:OnClick_SimulationMode");
            GetCanvas<UI_Canvas_Idle>().SequenceChange(UI_Canvas_Idle.SquenceType.Simulation);
        }

        // UI_Panel_BottomButtons panel_BottomButtons;
        bool onSensor;
        void OnClick_Sensor()
        {
            Debug.Log($"{button_Sensor}:OnClick_Sensor");
            if(onSensor)
                FindObjectOfType<Managers>().DeactiveSensorFlag();
            else
                FindObjectOfType<Managers>().ActiveSensorFlag();
            onSensor = !onSensor;
        }
    }
}