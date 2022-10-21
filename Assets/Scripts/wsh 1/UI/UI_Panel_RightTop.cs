using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
            base.Initialize();
            TryGetUIElement("Button_Home", out button_Home);
            TryGetUIElement("Button_SceneChange", out button_SceneChange);
            TryGetUIElement("Button_SimulationMode", out button_SimulationMode);
            TryGetUIElement("Button_Sensor", out button_Sensor);
            TryGetUIElement("Text_Data", out text_Date);

            button_Home.onClick.AddListener(OnClick_Home);
            button_SceneChange.onClick.AddListener(OnClick_SceneChange);
            button_SimulationMode.onClick.AddListener(OnClick_SimulationMode);
            button_Sensor.onClick.AddListener(OnClick_Sensor);
            panel_BottomButtons = UIManager.instance.GetCanvas<UI_Canvas_Bottom>().GetPanel<UI_Panel_BottomButtons>();
        }

        void OnClick_Home()
        {
            Debug.Log($"{button_Home}:OnClick_Home");
        }

        void OnClick_SceneChange()
        {
            Debug.Log($"{button_SceneChange}:OnClick_SceneChange");
        }

        void OnClick_SimulationMode()
        {
            Debug.Log($"{button_SceneChange}:OnClick_SimulationMode");
        }

        UI_Panel_BottomButtons panel_BottomButtons;
        void OnClick_Sensor()
        {
            Debug.Log($"{button_Sensor}:OnClick_Sensor");
            UIManager.instance.GetCanvas<UI_Canvas_Bottom>().PlayAnimation(panel_BottomButtons);
        }
    }
}