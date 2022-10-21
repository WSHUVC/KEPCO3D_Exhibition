using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using WSH.UI;

namespace WSH.Util
{
    [InitializeOnLoad]
    public static class HierachyTracker
    {
        static HierachyTracker()
        {
            EditorApplication.hierarchyChanged += UITracking;
        }

        static void UITracking()
        {
            var uis = Resources.FindObjectsOfTypeAll(typeof(GameObject));
            foreach(var ui in uis)
            {
                var obj = ui as GameObject;
                if (obj.GetComponent<EventSystem>())
                    continue;
                if (obj.GetComponent<UIBehaviour>())
                {
                    obj.TryAddComponent<UIScaler>();
                }
            }
        }
    }
}
