using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot;
    public float speed;
    private void OnEnable()
    {
        pos = transform.position;
        rot = transform.rotation;
    }
    public void Init()
    {
        transform.position = pos;
        transform.rotation = rot;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Init();
        }
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(0f, -Input.GetAxis("Mouse X") * speed, 0f, Space.World);
            transform.Rotate(Input.GetAxis("Mouse Y") * speed, 0f, 0f);
        }
    }
}
