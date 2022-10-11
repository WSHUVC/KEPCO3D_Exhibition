using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot;
    public GameObject map;
    private void OnEnable()
    {
        pos = transform.position;
        rot = transform.rotation;
    }
    Vector3 originMousePos;

    public float min;
    public float max;

    public void Init()
    {
        transform.position = pos;
        transform.rotation = rot;
        Camera.main.fieldOfView = max;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Init();
        }

        var fov = Camera.main.fieldOfView;
        fov -= Input.mouseScrollDelta.y;
        if (fov < min)
            fov = min;
        if (fov > max)
            fov = max;
        Camera.main.fieldOfView = fov;
    }
}
