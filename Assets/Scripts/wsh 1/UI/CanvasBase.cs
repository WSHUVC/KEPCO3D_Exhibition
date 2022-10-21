using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WSH.UI
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasBase : MonoBehaviour
    {
        [SerializeField] protected PanelBase[] panels;
        [SerializeField] protected UIBehaviour[] uiElements;
        protected Dictionary<string, List<UIBehaviour>> nameTable = new Dictionary<string, List<UIBehaviour>>();
        public virtual void Initialize()
        {
            panels = GetComponentsInChildren<PanelBase>();
            foreach(var p in panels)
            {
                p.Initialize();
            }   

            var temp = GetComponentsInChildren<UIBehaviour>();
            uiElements = temp.Where(t => t.gameObject.transform.parent == transform).ToArray();
            foreach (var ui in uiElements)
            {
                if (!nameTable.TryGetValue(ui.gameObject.name, out var list))
                    nameTable.Add(ui.gameObject.name, new List<UIBehaviour>());
                nameTable[ui.gameObject.name].Add(ui);
            }
        }

        Dictionary<Component, UIAnimation> animationTable = new Dictionary<Component, UIAnimation>();
        public void PlayAnimation(Component ui, bool rewind = false)
        {
            if(animationTable.TryGetValue(ui, out var animator))
            {
                animator.Play(rewind);
                return;
            }    
            animator = ui.gameObject.GetComponent<UIAnimation>();
            if (animator != null)
            {
                animationTable.Add(ui, animator);
                animator.Play(rewind);
            }
        }

        internal T GetPanel<T>() where T : PanelBase
        {
            foreach(var panel in panels)
            {
                if(panel is T)
                {
                    return panel as T;
                }
            }
            return null;
        }

        public bool TryGetPanel<T>(string name, out T result) where T: PanelBase
        {
            foreach(var p in panels)
            {
                if(p.name == name)
                {
                    result = p as T;
                    return true;
                }
            }
            result = null;
            return false;
        }

        public bool TryGetUIElement<T>(string name, out T result) where T : UIBehaviour 
        {
            if (nameTable.TryGetValue(name, out var element))
            {
                foreach (var e in element)
                {
                    if (e is T)
                    {
                        result = e as T;
                        return true;
                    }
                }
            }
            result = null;
            return false;
        }
        public virtual void Active()
        {
        }

        public virtual void Deactive()
        {
        }
    }
}
