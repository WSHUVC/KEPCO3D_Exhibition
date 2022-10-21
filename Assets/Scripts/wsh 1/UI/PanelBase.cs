using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace WSH.UI
{
    public class PanelBase : MonoBehaviour
    {
        protected UIBehaviour[] uiElements;
        protected Dictionary<string, List<UIBehaviour>> nameTable = new Dictionary<string, List<UIBehaviour>>();
        public virtual void Initialize()
        {
            var temp = GetComponentsInChildren<UIBehaviour>();
            uiElements = temp.Where(t => t.gameObject.transform.parent == transform).ToArray();
            foreach (var ui in uiElements)
            {
                if (!nameTable.TryGetValue(ui.gameObject.name, out var list))
                    nameTable.Add(ui.gameObject.name, new List<UIBehaviour>());
                nameTable[ui.gameObject.name].Add(ui);
            }
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


    }
}
