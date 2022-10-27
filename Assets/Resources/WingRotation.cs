using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingRotation : MonoBehaviour
{

    public float rotSpeed = 5f;

    void Update()
    {
        transform.Rotate(new Vector3(rotSpeed * Time.deltaTime, 0, 0));
    }
}
