using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas AlarmPanel_canvas;
    public MeshRenderer AlertCube_meshRenderer;
    public MeshRenderer AlertCube1_meshRenderer;

 
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEntered!");

        if (other.gameObject.name == "AlertCube")
        {
            Debug.Log("AlertCube Triggered");
            AlarmPanel_canvas.enabled = true;
            AlertCube_meshRenderer.enabled = true;
        }

        if (other.gameObject.name == "AlertCube1")
        {
            AlarmPanel_canvas.enabled = true;
            AlertCube1_meshRenderer.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "AlertCube")
        {
            AlarmPanel_canvas.enabled = false;
            AlertCube_meshRenderer.enabled = false;
        }

        if (other.gameObject.name == "AlertCube1")
        {
            AlarmPanel_canvas.enabled = false;
            AlertCube1_meshRenderer.enabled = false;
        }
    }
}
