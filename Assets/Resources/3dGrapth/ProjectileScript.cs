using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    public void Start()
    {
    }

    public void SetUpLine()
    {
        lineRenderer.positionCount = 2;
        Vector3 startPoistion = this.transform.position;
        Vector3 floorPoistion = new Vector3(this.transform.position.x, -1f, this.transform.position.z);

        lineRenderer.SetPosition(0, startPoistion);
        lineRenderer.SetPosition(1, floorPoistion);
        
    }
   
}
