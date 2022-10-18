using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WSH.Core.Util;
using WSH.UI;

namespace WSH.Core.Manager
{
    [Serializable]
    public class UIManager
    {
        public List<UIBase> allUI = new List<UIBase>();
        public UI_Flag prefab_Flag;
        public UI_Flag[] flags;
        public Tag_GP[] groundPoints;
        Canvas[] canvasis;

        public void Initialize()
        {
            Flaging();
            CanvasCheck();
        }
        void CanvasCheck()
        {
            canvasis = GameObject.FindObjectsOfType<Canvas>();
            var cam = Camera.main;
            foreach (var c in canvasis)
            {
                if (!c.worldCamera)
                    c.worldCamera = cam;
            }
        }
        void Flaging()
        {
            groundPoints = GameObject.FindObjectsOfType<Tag_GP>();
            flags = new UI_Flag[groundPoints.Length];
            for (int i = 0; i < groundPoints.Length; ++i)
            {
                flags[i] = GameObject.Instantiate(prefab_Flag);
                flags[i].Flagging(groundPoints[i]);
                flags[i].gameObject.name = $"Flag_{i}";
            }
        }

        delegate void ClickEvent<T>(T caller);

        public void OnClickEvent<T>(T eventCaller) where T : UIBase
        {
            Dictionary<Type, ClickEvent<T>> clickEventTable = new Dictionary<Type, ClickEvent<T>>();
            clickEventTable.Add(typeof(UI_Flag), OnClickFlag);

            clickEventTable?[eventCaller.GetType()](eventCaller);
        }
        void OnClickFlag<T>(T flag)
        {
            ActionController.instance.OnClickFlag(flag as UI_Flag);
        }
        public void OpenFlags()
        {
            foreach (var f in flags)
            {
                f.Open();
            }
        }

        internal void CloseFlags()
        {
            foreach (var f in flags)
            {
                f.Close();
            }
        }
    }
}