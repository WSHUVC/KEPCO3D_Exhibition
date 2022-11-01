using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WSH.UI
{
    public class UI_Canvas_Map : CanvasBase
    {
        public UI_Flag[] ui_Flags;
        public UI_Panel_Map panel_Map;
        public Button button_CloseMap;
        public UI_Canvas_Bottom canvas_Bottom;
        public UI_Canvas_LeftMenu canvas_Left;
        public UI_Canvas_RightMenu canvas_Right;
        Camera camera_Map;
        public override void Initialize()
        {
            base.Initialize();
            GetPanel(out panel_Map);
            canvas_Left = GetCanvas<UI_Canvas_LeftMenu>();
            canvas_Right = GetCanvas<UI_Canvas_RightMenu>();
            ui_Flags = FindObjectsOfType<UI_Flag>();
            camera_Map = GetComponentInChildren<Camera>();
            GetUIElement("Button_CloseMap", out button_CloseMap);
            button_CloseMap.onClick.AddListener(Deactive);

            canvas_Bottom = GetCanvas<UI_Canvas_Bottom>();
        }

        public bool isOn;
        public override void Active()
        {
            if (isOn)
            {
                Deactive();
                return;
            }
            base.Active();
            isOn = true;
            canvas_Left.Deactive();
            canvas_Right.Deactive();
            camera_Map.gameObject.SetActive(true);
            //canvas_Bottom.Deactive();
            panel_Map.Active();
        }

        public override void Deactive()
        {
            base.Deactive();
            isOn = false;
            camera_Map.gameObject.SetActive(false);
            panel_Map.Deactive();
        }
    }
}