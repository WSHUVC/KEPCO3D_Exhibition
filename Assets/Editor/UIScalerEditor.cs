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
        if (scaler.originSize== Vector2.zero)
            scaler.OriginSizeChange();
        scaler.SetScalePerRatio(ratio);
    }

    float ratio;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ratio = EditorGUILayout.Slider("SizeRatio",scaler.sizeRatio, 0,2f);
        if (ratio != scaler.sizeRatio)
            scaler.SetScalePerRatio(ratio);
        if (GUILayout.Button("OriginChange"))
            scaler.OriginSizeChange();
    }
}
