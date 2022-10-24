using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


// TargetController는 전달받은 위치로 이동시키는 역할만 한다.

public class TargetController : MonoBehaviour
{
    

    // Node 관련 
    public GameObject way;
    public Transform[] nodes; 
    public List<Vector3> nodesPositions = new List<Vector3>();


    private void Start()
    {

    }

    void Update()
    {

    }

    public void moveTargetAsync(Transform startPoint, Transform targetPoint, GameObject selectedway, bool Reverse)
    {
        nodesPositions.Clear();
        // 시작지점 추가
        // 길 추가
        nodes = selectedway.GetComponentsInChildren<Transform>();
        for (int i = 1; i < nodes.Length; i++) // 자식 오브젝트만 뽑기위해 1부터 
        {
            nodesPositions.Add(nodes[i].position);
            //Debug.Log(nodesPositions[i]);
        }

        //방향 설정(역방향/순반향)
        if (Reverse) nodesPositions.Reverse();
        // 목적지점 추가 
        nodesPositions.Add(targetPoint.position);
        //시작점 추가
        nodesPositions.Insert(0, startPoint.position);
      
        foreach (var routePoint in nodes)
        {
            Debug.Log($" Beizer Move Order route -> {routePoint.name}");
        }
        // 무빙 시작
        BeizerMove_Test();
    }


    public void moveToSensor(Transform ts)
    {
        transform.position =ts.position;
    }


    IEnumerator LerpPosition(Vector3[] targetPositions, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Lerp(targetPositions, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }



    public Vector3 Lerp(Vector3[] points, float process)
    {
        Vector3[] lerpPoints;
        Vector3[] prevLerps = points;
        while (prevLerps.Length > 1)
        {
            lerpPoints = new Vector3[prevLerps.Length - 1];
            for (int i = 0; i < lerpPoints.Length; ++i)
            {
                lerpPoints[i] = Vector3.Lerp(prevLerps[i], prevLerps[i + 1], process);
            }
            prevLerps = lerpPoints;
        }
        return prevLerps[0];
    }
    public void BeizerMove_Test()
    {
        StartCoroutine(LerpPosition(nodesPositions.ToArray(), 3));
 
    }

    public void BeizerMoveRevert_Test()
    {
        nodesPositions.Reverse();

    }
}
