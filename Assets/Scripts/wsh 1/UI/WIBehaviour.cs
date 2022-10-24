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
        public static List<WIBehaviour> customUIList = new List<WIBehaviour>();
        public static Dictionary<Type, CanvasBase> canvasTable = new Dictionary<Type, CanvasBase>();
        public static Dictionary<Type, List<PanelBase>> panelTable = new Dictionary<Type, List<PanelBase>>();
        public static Dictionary<WIBehaviour, List<UIBehaviour>> uiElementsTable = new Dictionary<WIBehaviour, List<UIBehaviour>>();
        void Awake()
        {
            customUIList.Add(this);
        }

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

        public bool GetUIElement<T>(string name, out T result) where T : UIBehaviour
        {
            if (uiElementsTable.TryGetValue(this, out var list))
            {
                foreach(var l in list)
                {
                    if(l is T && l.gameObject.name.Equals(name))
                    {
                        result = l as T;
                        return true;
                    }
                }
            }
            var load = GetComponentsInChildren<UIBehaviour>();
            if (load == null || load.Length == 0)
            {
                result = null;
                return false;
            }

            var reload = load.Where(l => l.transform.parent == transform || l.transform == transform).ToList();
            if(!uiElementsTable.ContainsKey(this))
                uiElementsTable.Add(this, reload);

            foreach(var r in reload)
            {
                if(r is T && r.gameObject.name.Equals(name))
                {
                    result = r as T;
                    return true;
                }
            }

            result = null;
            return false;
        }


        public T[] GetUIElements<T>() where T : UIBehaviour
        {
            return null;
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
