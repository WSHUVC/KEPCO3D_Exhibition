using Knife.HDRPOutline.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas AlarmPanel_canvas;
    public MeshRenderer alertCube;
    public MeshRenderer alertCube1;
    public SpriteRenderer sprite_PlayerMarker;
    public Color color_AlertMarker;
    public Color color_originMarker;
    private void Awake()
    {
        sprite_PlayerMarker = GetComponentInChildren<SpriteRenderer>();
        color_originMarker = sprite_PlayerMarker.color;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEntered!");

        if (other.gameObject.name == "AlertCube")
        {
            Debug.Log("AlertCube Triggered");
            Alert(alertCube, true);
        }

        if (other.gameObject.name == "AlertCube1")
        {
            Alert(alertCube1, true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AlertCube")
        {
            Alert(alertCube, false);
        }

        if (other.gameObject.name == "AlertCube1")
        {
            Alert(alertCube1, false);
        }
    }

    void Alert(MeshRenderer mesh, bool isOn)
    {
        //mesh.enabled = isOn;
        AlarmPanel_canvas.enabled = isOn;
        mesh.GetComponent<OutlineObject>().enabled = isOn;
        if (isOn)
            sprite_PlayerMarker.color = color_AlertMarker;
        else
            sprite_PlayerMarker.color = color_originMarker;
    }
}
