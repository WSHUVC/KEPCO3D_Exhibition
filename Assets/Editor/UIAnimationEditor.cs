using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using WSH.UI;

[CanEditMultipleObjects]
[CustomEditor(typeof(UIAnimator))]
public class UIAnimationEditor : Editor
{
    UIAnimator anim;
    private void OnEnable()
    {
        anim = target as UIAnimator;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("SetStartState"))
            anim.SaveOrigin();
        if (GUILayout.Button("SetTargetState"))
            anim.SaveTarget();
        
        if (GUILayout.Button("Reset"))
            anim.SetOrigin();
        if (GUILayout.Button("PreviewTargetState"))
            anim.SetTarget();
    }
}
