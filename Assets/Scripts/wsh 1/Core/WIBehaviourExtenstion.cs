
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WSH.UI
{
    public static class WIBehaviourExtenstion
    {
        static Dictionary<Type, CanvasBase> canvasTable = new Dictionary<Type, CanvasBase>();
        static Dictionary<Type, List<PanelBase>> panelTable = new Dictionary<Type, List<PanelBase>>();
        
        public static T GetCanvas<T>(this Transform root) where T : CanvasBase
        {
            var canvasType = typeof(T);
            if (canvasTable.TryGetValue(canvasType, out var result))
                return result as T;

            var finding = GameObject.FindObjectOfType<T>();
            if (finding == null)
                return null;

            canvasTable.Add(canvasType, finding);
            return finding;
        }
        public static bool GetPanel<T>(this Transform root, out T result) where T : PanelBase
        {
            result = null;
            GetPanels<T>(root, out var panels);
            if (panels == null || panels.Count == 0)
                return false;
            result = panels[0] as T;
            return true;
        }
        public static bool GetPanels<T>(this Transform root, out List<T> result) where T : PanelBase
        {
            var panelType = typeof(T);
            if (panelTable.TryGetValue(panelType, out var list))
            {
                result = list.ConvertAll(new Converter<PanelBase, T>(PanelTypeConvertor<PanelBase, T>));
                return true;
            }
            var finding = GameObject.FindObjectsOfType<T>().ToList();
            if (finding == null || finding.Count == 0)
            {
                result = null;
                return false;
            }
            result = finding;
            var data = finding.ConvertAll(new Converter<T, PanelBase>(PanelTypeConvertor<T, PanelBase>));
            panelTable.Add(panelType, data);
            return false;
        }
        static T2 PanelTypeConvertor<T, T2>(T origin) where T : WIBehaviour where T2 : WIBehaviour
        {
            return origin as T2;
        }
    }
}
