using UnityEngine;
using UnityEditor;
using WSH.Core;
using WSH.Core.Manager;

[CustomEditor(typeof(Managers))]
public class ManagersEditor : Editor
{
    Managers m;

    private void OnEnable()
    {
        m = target as Managers;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("FindTagObjects"))
            m.FindTagObjects();
        if (GUILayout.Button("SetDefaltMaterial"))
            m.SetDefaultMaterial();
        if (GUILayout.Button("PlaceFlagging"))
            m.PlaceFlagging();
        if (GUILayout.Button("SensorFlagging"))
            m.SensorFlagging();
        if (GUILayout.Button("FlagCleaning"))
            m.FlagCleaning();
        if (GUILayout.Button("Active Place Flag"))
        {
            m.ActivePlaceFlag();
        }
        if (GUILayout.Button("Deactive Place Flag"))
        {
            m.DeactivePlaceFlag();
        }
        if (GUILayout.Button("Active Sensor Flag"))
        {
            m.ActiveSensorFlag();
        }
        if (GUILayout.Button("Deactive Sensor Flag"))
        {
            m.DeactiveSensorFlag();
        }
    }
}
