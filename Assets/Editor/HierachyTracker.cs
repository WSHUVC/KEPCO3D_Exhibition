﻿using UnityEditor;
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
            var uis = Resources.FindObjectsOfTypeAll(typeof(RectTransform));
            foreach(var ui in uis)
            {
                var obj = ui as RectTransform;
                if (obj.gameObject.TryGetComponent<UIBehaviour>(out var ub))
                {
                    obj.gameObject.TryAddComponent<UIScaler>();
                    var scalers = obj.gameObject.GetComponents<UIScaler>();
                    if (scalers.Length > 1)
                    {
                        for (int i = 1; i < scalers.Length; ++i)
                        {
                            GameObject.DestroyImmediate(scalers[i]);
                        }
                    }
                }
            }
        }
    }
}
