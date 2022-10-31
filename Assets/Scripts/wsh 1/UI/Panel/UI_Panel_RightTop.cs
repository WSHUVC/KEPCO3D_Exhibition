using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        Button button_MiniMap;

        public override void Initialize()
        {
            GetUIElement("Button_Home", out button_Home);
            GetUIElement("Button_SceneChange", out button_SceneChange);
            GetUIElement("Button_SimulationMode", out button_SimulationMode);
            GetUIElement("Button_MiniMap", out button_MiniMap);
            GetUIElement("Text_Date", out text_Date);

            button_Home.onClick.AddListener(OnClick_Home);
            button_SceneChange.onClick.AddListener(OnClick_SceneChange);
            button_SimulationMode.onClick.AddListener(OnClick_SimulationMode);
            button_MiniMap.onClick.AddListener(OnClick_MiniMap);
            button_SceneChange.gameObject.SetActive(false);
            StartCoroutine(Timer());
        }

        IEnumerator Timer()
        {
            while (true)
            {
                text_Date.SetText(DateTime.Now.ToString());
                yield return new WaitForSecondsRealtime(1f);
            }
        }

        void OnClick_Home()
        {
            Debug.Log($"{button_Home}:OnClick_Home");
            GetCanvas<UI_Canvas_Idle>().IdleOn();
            //button_Sensor.gameObject.SetActive(true);
            //button_SceneChange.gameObject.SetActive(true);
            button_SimulationMode.gameObject.SetActive(true);
        }

        void OnClick_SceneChange()
        {
            Debug.Log($"{button_SceneChange}:OnClick_SceneChange");
        }

        void OnClick_SimulationMode()
        {
            Debug.Log($"{button_SceneChange}:OnClick_SimulationMode");
            GetCanvas<UI_Canvas_Bottom>().Deactive();
            GetCanvas<UI_Canvas_LeftMenu>().Deactive();
            GetCanvas<UI_Canvas_RightMenu>().Deactive();
            GetCanvas<UI_Canvas_Idle>().SequenceChange(UI_Canvas_Idle.SquenceType.Simulation);
            FindObjectOfType<UIManager>().OutlineOff();
            //button_MiniMap.gameObject.SetActive(false);
            button_SceneChange.gameObject.SetActive(false);
            button_SimulationMode.gameObject.SetActive(false);
        }

        // UI_Panel_BottomButtons panel_BottomButtons;
        void OnClick_MiniMap()
        {
            Debug.Log($"{button_MiniMap}:OnClick_MiniMap");
        }
    }
}