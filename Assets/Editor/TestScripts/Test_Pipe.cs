using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Pipe : MonoBehaviour
{
    public Vector3 oPos;
    public Vector3 oRot;
    public Vector3 oScale;
    public Vector3 tPos;
    public Vector3 tRot;
    public Vector3 tScale;
    public Test_Pipe next;
    public void SaveOrigin()
    {
        oPos = transform.localPosition;
        oRot = transform.localRotation .eulerAngles;
        oScale = transform.localScale;
    }

    public void SaveTarget()
    {
        tPos = transform.localPosition;
        tRot = transform.localRotation.eulerAngles;
        tScale = transform.localScale;
    }

    public void SetOrigin()
    {
        transform.localPosition = oPos;
        transform.localRotation = Quaternion.Euler(oRot);
        transform.localScale = oScale;
    }

    public void SetTarget()
    {
        transform.localPosition = tPos;
        transform.localRotation = Quaternion.Euler(tRot);
        transform.localScale = tScale;
    }

    public float speed;
    public void NextAnimPlay()
    {
        next.Play();
    }
    public void Play()
    {
        StartCoroutine(Anim());
    }

    float process;
    public Action endEvent;
    IEnumerator Anim()
    {
        Debug.Log($"{name} Play");
        process = 0f;
        transform.localPosition = oPos;
        transform.localRotation = Quaternion.Euler(oRot);
        transform.localScale = oScale;
        while (process < 1f)
        {
            process += Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(oPos, tPos, process);
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(oRot, tRot, process));
            transform.localScale = Vector3.Lerp(oScale, tScale, process);
            yield return null;
        }
        endEvent();
        Debug.Log($"{name} End");
    }
}
