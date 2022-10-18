using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace WSH.UI
{
    public class GroupOption : MonoBehaviour
    {
        public enum FlagOption
        {
            All,
            Total,
            Nothing,
        }

        public FlagOption flagingOption;

        public List<Transform> groupObjects = new List<Transform>();
        Map map;
        internal void LoadGroupObjects()
        {
            map = FindObjectOfType<Map>();
            if (groupObjects.Count == 0)
            {
                groupObjects.AddRange(GetComponentsInChildren<Transform>());
            }
        }
    }
}