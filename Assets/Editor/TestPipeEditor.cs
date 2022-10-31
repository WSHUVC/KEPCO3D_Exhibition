using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test_Pipe))]
public class TestPipeEditor : Editor
{
    Test_Pipe pipe;
    private void OnEnable()
    {
        pipe = target as Test_Pipe;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("SaveOrigin"))
            pipe.SaveOrigin();
        if(GUILayout.Button("SaveTarget"))
            pipe.SaveTarget();
        if(GUILayout.Button("SetOrigin"))
            pipe.SetOrigin();
        if (GUILayout.Button("SetTarget"))
            pipe.SetTarget();
    }
}
