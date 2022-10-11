using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class Bilboard : MonoBehaviour
{
    public Transform owner;
    public Transform cam;
    public Button pointButton;
    private void OnEnable()
    {
        cam = Camera.main.transform;
        owner = GetComponentInParent<Tag_Owner>().transform;
        pointButton = GetComponentInChildren<Button>();
        pointButton.onClick.RemoveAllListeners();
        pointButton.onClick.AddListener(OnClickPoint);
    }
    private void Update()
    {
        transform.LookAt(transform.position + cam.rotation * Vector3.forward,
            cam.rotation * Vector3.up);
    }

    void OnClickPoint()
    {
    }
}
