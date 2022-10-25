using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WSH.Core.Manager;

namespace WSH.UI.InspectorEditor
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        UIManager um;
        private void OnEnable()
        {
            um = target as UIManager;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("ResolutionPatch"))
                um.ResolutionPatch();
        }
    }
}
