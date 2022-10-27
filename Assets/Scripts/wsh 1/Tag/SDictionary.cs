using System;
using System.Collections.Generic;
using UnityEngine;

namespace WSH.Util
{
    [Serializable]
    public struct SKeyValuePair<T, T2> 
    {
        [SerializeField] public T Key;
        [SerializeField] public T2 Value;
        public SKeyValuePair(T key, T2 value)
        {
            Key = key;
            Value = value;
        }
    }

    /// <summary>
    /// SerializeDictionary
    /// </summary>
    /// 
    [System.Serializable]
    public class SDictionary<T, T2>
    {
        protected Dictionary<T, T2> table = new Dictionary<T, T2>();
        [SerializeField] public List<SKeyValuePair<T, T2>> datas=new List<SKeyValuePair<T, T2>>();
        
        public T2 this[T key] => table[key];
        public bool TryAdd(T key, T2 value)
        {
            if (table.ContainsKey(key))
                return false;

            table.Add(key, value);
            datas.Add(new SKeyValuePair<T, T2>(key, value));
            return true;
        }

        public bool Remove(T key)
        {
            if (!table.ContainsKey(key))
                return false;
            for(int i = 0; i < datas.Count; ++i)
            {
                if (datas[i].Key.Equals(key))
                {
                    datas.RemoveAt(i);
                    table.Remove(key);
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            table.Clear();
            datas.Clear();
        }
    }
}