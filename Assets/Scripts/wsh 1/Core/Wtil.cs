using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WSH.Util
{
    public static class Wtil
    {
        static Dictionary<Transform, Dictionary<string, List<UIBehaviour>>> childElementTable =
    new Dictionary<Transform, Dictionary<string, List<UIBehaviour>>>(); 
        public static bool TryGetValue<T1, T2>
        (this Dictionary<T1,T2> table, T1 key, out T2 result, Func<T1, T1, bool> groupCheck)
        {
            if (table.TryGetValue(key, out result))
            {
                return true;
            }

            //value = FindObjectsOfType<T2>().Where;
            if (result == null)
                return false;

            table.Add(key, result);
            result = default(T2);
            return true;
        }

        public static bool GetUIElement<T>(this Transform root, string targetName, out T result) where T : UIBehaviour
        {
            result = null;
            if (childElementTable.TryGetValue(root, out var childNameTable))
            {
                if (childNameTable.TryGetValue(targetName, out var uiList))
                {
                    foreach (var t in uiList)
                    {
                        if (t is T)
                        {
                            result = t as T;
                            return true;
                        }
                    }
                }
            }
            if (!childElementTable.ContainsKey(root))
                childElementTable.Add(root, new Dictionary<string, List<UIBehaviour>>());
            var childs = root.GetComponentsInChildren<UIBehaviour>();
            foreach (var c in childs)
            {
                var childName = c.gameObject.name;
                if (!childElementTable[root].TryGetValue(childName, out var childUIList))
                {
                    childElementTable[root].Add(childName, new List<UIBehaviour>());
                }
                childElementTable[root][childName].Add(c);
                if (c is T && c.gameObject.name.Equals(targetName))
                {
                    result = c as T;
                }
            }

            return result != null;
        }

        /// <summary>
        /// 있으면 추가하지 않는다
        /// Do not allow duplicates
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T TryAddComponent<T>(this GameObject target) where T : Component
        {
            if (target.TryGetComponent<T>(out var result))
                return result;

            return target.AddComponent<T>();
        }
    }
}