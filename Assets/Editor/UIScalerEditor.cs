using WSH.UI;
using UnityEditor;
using UnityEngine;
[CanEditMultipleObjects]
[CustomEditor(typeof(UIScaler))]
public class UIScalerEditor : Editor
{
    UIScaler scaler;
    private void OnEnable()
    {
        scaler = target as UIScaler;
        ratio = 1f;
    }

    float ratio;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ratio = EditorGUILayout.Slider("SizeRatio",scaler.sizeRatio, 0f,1f);
        if (ratio != scaler.sizeRatio)
            scaler.SetScalePerRatio(ratio);
        if (GUILayout.Button("OriginChange"))
            scaler.OriginSizeChange();
    }
}
