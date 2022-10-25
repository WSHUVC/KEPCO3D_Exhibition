using Knife.HDRPOutline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using WSH.UI;
using Debug = WSH.Util.Debug;

namespace WSH.Core.Manager
{
    public class UIManager : MonoBehaviour
    {
        public CanvasBase[] canvasis;
        public int highLightMaterialIndex;

        Managers managers;
        WIBehaviour[] allUIs;
        UI_Canvas_Idle canvas_Idle;
        UI_Canvas_Tabs canvas_Tabs;
        UI_Canvas_TopBar canvas_TopBar;
        UI_Canvas_Bottom canvas_Bottom;

        public void OutlineOff()
        {
            if (outlineObjects.Count == 0)
                outlineObjects = FindObjectsOfType<OutlineObject>().ToList();

            foreach(var o in outlineObjects)
            {
                o.enabled = false;
            }
        }

        public void OutlineOn()
        {
            if (outlineObjects.Count == 0)
                outlineObjects = FindObjectsOfType<OutlineObject>().ToList();

            foreach (var o in outlineObjects)
            {
                o.enabled = true;
            }
        }

        public Material[] highLightMaterials;
        public Material outlineMaterial;
        public List<OutlineObject> outlineObjects = new List<OutlineObject>();
        public Color outlineColor;
        public Volume outlineOption;

        private void Awake()
        {
            var option = outlineOption.sharedProfile.components;
            foreach(var o in option)
            {
                if (o.name.Equals("HDRPOutline"))
                {
                }
            }
            var trashs = FindObjectsOfType<UIManager>();
            if (trashs.Length > 1)
            {
                for(int i = 1; i < trashs.Length; ++i)
                {
                    var target = trashs[i];
                    DestroyImmediate(target.gameObject);
                }
                return;
            }
            managers = FindObjectOfType<Managers>();
            canvasis = FindObjectsOfType<CanvasBase>();
        }

        public void IdleOff()
        {
            Debug.Log($"{name}:IdleOff");
            //canvas_Tabs.Active();
            canvas_TopBar.Active();
            canvas_Bottom.Active();
            managers.ActivePlaceFlag();
        }

        public Material GetHighLightMaterial(UI_Flag flag)
        {
            return highLightMaterials[highLightMaterialIndex];
        }

        public void IdleOn()
        {
            Debug.Log($"{name}:IdleOn");
            canvas_TopBar.Deactive();
            canvas_Bottom.Deactive();
            canvas_Tabs.Deactive();
            managers.DeactivePlaceFlag();
            managers.DeactiveSensorFlag();
        }
        void Start()
        {
            allUIs = FindObjectsOfType<WIBehaviour>();
            //Debug.Log($"UIManagerStart{i++}");
            foreach (var c in allUIs)
            {
                c.Initialize();
                if (c is UI_Canvas_Idle)
                {
                    canvas_Idle = c as UI_Canvas_Idle;
                    canvas_Idle.button_IdleOff.onClick.AddListener(IdleOff);
                }
                else if (c is UI_Canvas_TopBar)
                {
                    canvas_TopBar = c as UI_Canvas_TopBar;
                }
                else if (c is UI_Canvas_Bottom)
                {
                    canvas_Bottom = c as UI_Canvas_Bottom;
                }
                else if (c is UI_Canvas_Tabs)
                {
                    canvas_Tabs = c as UI_Canvas_Tabs;
                    canvas_Tabs.Deactive();
                }
            }
        }
    }
}