using UnityEditor;
using UnityEngine;

namespace WSH.Util.InspectorEditor
{
    [CustomEditor(typeof(LabelPrinter))]
    public class LabelPrinterEditor : Editor
    {
        LabelPrinter printer;
        void OnEnable()
        {
            printer = target as LabelPrinter;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
  //          if (GUILayout.Button("FindLabels"))
//                printer.FindLabels();
        }
    }
}