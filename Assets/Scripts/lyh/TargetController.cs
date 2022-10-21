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
        nodesPositions.Clear();
    }

    void Update()
    {

    }

    public void moveTargetAsync(Transform startPoint, Transform targetPoint, GameObject selectedway, bool Reverse)
    {
        nodesPositions.Clear();
        // 시작지점
        transform.position = startPoint.position;
        nodesPositions.Add(startPoint.position);
        // 길
        nodes = selectedway.GetComponentsInChildren<Transform>();
        for (int i = 1; i < nodes.Length; i++) // 자식 오브젝트만 뽑기위해 1부터 
        {
            nodesPositions.Add(nodes[i].position);
            //Debug.Log(nodesPositions[i]);
        }
        // 목적지
        nodesPositions.Add(targetPoint.position);
        // 방향 설정 
        if(Reverse)
        {
            //역방향
            BeizerMoveRevert_Test();
            BeizerMove_Test();
        } else
        {
            //순방향
            BeizerMove_Test();
        }
      
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
        StartCoroutine(LerpPosition(nodesPositions.ToArray(), 5));
    }

    public void BeizerMoveRevert_Test()
    {
        nodesPositions.Reverse();

    }
}
