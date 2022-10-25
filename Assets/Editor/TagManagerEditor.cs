using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using WSH.Util;

namespace WSH.UI.InspectorEditor
{
    [CustomEditor(typeof(TagManager))]
    public class TagManagerEditor : Editor
    {
        TagManager tm;

        private void OnEnable()
        {
            tm = target as TagManager;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("SetGroupFile"))
                tm.SetGroupFile();

            if (GUILayout.Button("SaveGroupSetting"))
                tm.SaveGroupSetting();
        }

    }
}
