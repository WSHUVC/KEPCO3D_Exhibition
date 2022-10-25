using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using System.Linq;
using Debug = WSH.Util.Debug;

namespace WSH.UI
{
    public class WIBehaviour : MonoBehaviour
    {
        public static Dictionary<Type, CanvasBase> canvasTable = new Dictionary<Type, CanvasBase>();
        public static Dictionary<Type, List<PanelBase>> panelTable = new Dictionary<Type, List<PanelBase>>();
        public static Dictionary<WIBehaviour, List<UIBehaviour>> uiElementsTable = new Dictionary<WIBehaviour, List<UIBehaviour>>();

        public T GetCanvas<T>() where T : CanvasBase
        {
            var canvasType = typeof(T);
            if (canvasTable.TryGetValue(canvasType, out var result))
                return result as T;

            var finding = FindObjectOfType<T>();
            if (finding == null)
                return null;

            canvasTable.Add(canvasType, finding);
            return finding;
        }
       
        public bool GetPanel<T>(out T result) where T : PanelBase
        {
            result = null;
            GetPanels<T>(out var panels);
            if (panels==null || panels.Count == 0)
                return false;
            result = panels[0] as T;
            return true;
        }
        public bool GetPanels<T>(out List<T> result) where T : PanelBase
        {
            var panelType = typeof(T);
            if (panelTable.TryGetValue(panelType, out var list))
            {
                result = list.ConvertAll(new Converter<PanelBase, T>(PanelTypeConvertor<PanelBase,T>));
                return true;
            }
            var finding = FindObjectsOfType<T>().ToList();
            if(finding==null || finding.Count== 0)
            {
                result = null;
                return false;
            }
            result = finding;
            var data = finding.ConvertAll(new Converter<T, PanelBase>(PanelTypeConvertor<T, PanelBase>));
            panelTable.Add(panelType, data);
            return false;
        }
        T2 PanelTypeConvertor<T, T2>(T origin) where T : WIBehaviour where T2 : WIBehaviour
        {
            return origin as T2;
        }

        static Dictionary<WIBehaviour, Dictionary<string, List<UIBehaviour>>> childElementTable = 
            new Dictionary<WIBehaviour, Dictionary<string, List<UIBehaviour>>>();
        public bool GetUIElement<T>(string targetName, out T result) where T : UIBehaviour
        {
            result = null;
            if(childElementTable.TryGetValue(this, out var childNameTable))
            {
                if(childNameTable.TryGetValue(targetName, out var uiList))
                {
                    foreach(var t in uiList)
                    {
                        if(t is T)
                        {
                            result = t as T;
                            return true;
                        }
                    }
                }
            }
            if(!childElementTable.ContainsKey(this))
                childElementTable.Add(this, new Dictionary<string, List<UIBehaviour>>());
            var childs = gameObject.GetComponentsInChildren<UIBehaviour>().ToList();
            foreach (var c in childs)
            {
                var childName = c.gameObject.name;
                if (!childElementTable[this].TryGetValue(childName, out var childUIList))
                {
                    childElementTable[this].Add(childName, new List<UIBehaviour>());
                }
                childElementTable[this][childName].Add(c);
                if (c is T && c.gameObject.name.Equals(targetName))
                {
                    result = c as T;
                }
            }

            return result != null;
        }
        protected virtual void OnEnable() { }
        public virtual void Active() { }
        public virtual void Deactive() { }
        public virtual void Initialize() { }
        protected virtual void OnDisable() { }

        public virtual void RewindAnimation(WIBehaviour animator)
        {
            animator.GetComponent<UIAnimator>().Rewind();
        }
        public virtual void PlayAnimation(WIBehaviour animator)
        {
            animator.GetComponent<UIAnimator>().Play();
        }
        public virtual void PlayAnimation(UIBehaviour ui)
        {
            ui.GetComponent<UIAnimator>().Play();
        }
        public virtual void RewindAnimation(UIBehaviour ui)
        {
            ui.GetComponent<UIAnimator>().Rewind();
        }
        public virtual void RewindAnimation()
        {
            GetComponent<UIAnimator>().Rewind();
        }
        public virtual void PlayAnimation()
        {
            GetComponent<UIAnimator>().Play();
        }
    }
}
