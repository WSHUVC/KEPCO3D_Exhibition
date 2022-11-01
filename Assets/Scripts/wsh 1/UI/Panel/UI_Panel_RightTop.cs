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
        Button button_Electric;
        Button button_SimulationMode;
        Button button_MiniMap;
        UI_Canvas_Map canvas_Map;
        ElecTrigger elec => GetCanvas<UI_Canvas_Idle>().electricSequence.GetComponentInChildren<ElecTrigger>();
        public override void Initialize()
        {
            GetUIElement("Button_Home", out button_Home);
            GetUIElement("Button_Electric", out button_Electric);
            GetUIElement("Button_SimulationMode", out button_SimulationMode);
            GetUIElement("Button_MiniMap", out button_MiniMap);
            GetUIElement("Text_Date", out text_Date);
            canvas_Map = GetCanvas<UI_Canvas_Map>();

            button_Home.onClick.AddListener(OnClick_Home);
            button_MiniMap.onClick.AddListener(OnClick_MiniMap);
            button_Electric.onClick.AddListener(OnClick_Electric);
            button_SimulationMode.onClick.AddListener(OnClick_SimulationMode);
            canvas_Map.Deactive();
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
            button_MiniMap.gameObject.SetActive(true);
            button_Electric.gameObject.SetActive(true);
            button_SimulationMode.gameObject.SetActive(true);
            canvas_Map.Deactive();
            elec.Stop();
        }

        void ActiveSimulation(UI_Canvas_Idle.SquenceType sequenceType)
        {
            canvas_Map.Deactive();
            button_Electric.gameObject.SetActive(false);
            button_SimulationMode.gameObject.SetActive(false);

            GetCanvas<UI_Canvas_Bottom>().Deactive();
            GetCanvas<UI_Canvas_LeftMenu>().Deactive();
            GetCanvas<UI_Canvas_RightMenu>().Deactive();
            GetCanvas<UI_Canvas_Idle>().SequenceChange(sequenceType);
            FindObjectOfType<UIManager>().OutlineOff();
        }
        void OnClick_Electric()
        {
            Debug.Log($"{button_Electric}:OnClick_Electric");
            ActiveSimulation(UI_Canvas_Idle.SquenceType.Electric);
            elec.Play();
        }

        void OnClick_SimulationMode()
        {
            Debug.Log($"{button_Electric}:OnClick_SimulationMode");
            ActiveSimulation(UI_Canvas_Idle.SquenceType.Simulation);
            elec.Stop();
        }

        void OnClick_MiniMap()
        {
            Debug.Log($"{button_MiniMap}:OnClick_MiniMap");
            canvas_Map.Active();
            FindObjectOfType<UIManager>().OutlineOff();
        }
    }
}