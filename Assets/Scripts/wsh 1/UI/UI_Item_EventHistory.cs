using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace WSH.UI
{
    public class UI_Item_EventHistory : UIBase
    {
        Button button_Item;
        Image image_State;
        TextMeshProUGUI text_FactilityName;
        TextMeshProUGUI text_SensorName;
        TextMeshProUGUI text_IssueDate;
        TextMeshProUGUI text_Diagnosis;
        public int index;

        public override void Initialize()
        {
            base.Initialize();
            GetUIElement(name, out button_Item);
            GetUIElement("Text_FactilityName", out text_FactilityName);
            GetUIElement("Text_SensorName", out text_SensorName);
            GetUIElement("Text_IssueDate", out text_IssueDate);
            GetUIElement("Text_Diagnosis", out text_Diagnosis);
            GetUIElement("Image_State", out image_State);
            button_Item.onClick.AddListener(OnClick_Item);
            var state = (SensorState)Random.Range(0, 3);
            StateChange(state);
            text_FactilityName.SetText($"설비명_{index}");
            text_SensorName.SetText($"센서_{index}");
            var date = DateTime.Now;
            date = date.AddSeconds(Random.Range(-1000f, 1000f));
            text_IssueDate.SetText($"{date}");
            text_Diagnosis.SetText($"{state}");
        }
        public enum SensorState
        {
            Normal,
            Warning,
            Error,
        }
        public SensorState state;
        void StateChange(SensorState state)
        {
            this.state = state;
            switch (state)
            {
                case SensorState.Normal:
                    image_State.color = Color.green;
                    break;
                case SensorState.Warning:
                    image_State.color = Color.yellow;
                    break;
                case SensorState.Error:
                    image_State.color = Color.red;
                    break;
            }
        }

        public void ConnectSensorButton(UI_Button_PlaceSensor button)
        {
            button?.button_PlaceSensor?.onClick.AddListener(OnClick_Item);
        }

        public void OnClick_Item()
        {
            Debug.Log($"OnClick_{name}");
            if(index<3)
            FindObjectOfType<QuadScript>().DrawGraph(index);
            else
            FindObjectOfType<QuadScript>().DrawGraph(Random.Range(0,3));
        }
    }
}