using System;
using System.Collections.Generic;
using UnityEngine;
using WSH.UI;
using Debug = WSH.Util.Debug;

namespace WSH.Core.Manager
{
    public class UIManager : MonoBehaviour
    {
        Managers managers;
        public CanvasBase[] canvasis;
        public static UIManager instance;
        private void Awake()
        {
            instance = this;
            managers = FindObjectOfType<Managers>();
            canvasis = FindObjectsOfType<CanvasBase>();
            foreach(var c in canvasis)
            {
                c.Initialize();
                if (c is UI_Canvas_Idle)
                {
                    canvas_Idle = c as UI_Canvas_Idle;
                    canvas_Idle.button_IdleOff.onClick.AddListener(IdleOff);
                }
                else if(c is UI_Canvas_TopBar)
                {
                    canvas_TopBar = c as UI_Canvas_TopBar;
                }
                else if(c is UI_Canvas_Bottom)
                {
                    canvas_Bottom = c as UI_Canvas_Bottom;
                }
            }
        }
        UI_Canvas_Idle canvas_Idle;
        UI_Canvas_TopBar canvas_TopBar;
        UI_Canvas_Bottom canvas_Bottom;

        public List<GameObject> allUI = new List<GameObject>();
        public void IdleOff()
        {
            Debug.Log($"{name}:IdleOff");
            canvas_TopBar.Active();
            //canvas_Bottom.Active();
            managers.ActivePlaceFlag();
        }
        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            canvas_TopBar.Deactive();
            canvas_Bottom.Deactive();
            managers.DeactivePlaceFlag();
            managers.DeactiveSensorFlag();
        }

        internal CanvasBase GetCanvas<T>() where T : CanvasBase
        {
            foreach(var canvas in canvasis)
            {
                if(canvas is T)
                {
                    return canvas as T;
                }
            }
            return null;
        }
    }
}