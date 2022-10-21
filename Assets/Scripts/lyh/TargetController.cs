using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


// TargetController�� ���޹��� ��ġ�� �̵���Ű�� ���Ҹ� �Ѵ�.

public class TargetController : MonoBehaviour
{
    

    // Node ���� 
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
        // ��������
        transform.position = startPoint.position;
        nodesPositions.Add(startPoint.position);
        // ��
        nodes = selectedway.GetComponentsInChildren<Transform>();
        for (int i = 1; i < nodes.Length; i++) // �ڽ� ������Ʈ�� �̱����� 1���� 
        {
            nodesPositions.Add(nodes[i].position);
            //Debug.Log(nodesPositions[i]);
        }
        // ������
        nodesPositions.Add(targetPoint.position);
        // ���� ���� 
        if(Reverse)
        {
            //������
            BeizerMoveRevert_Test();
            BeizerMove_Test();
        } else
        {
            //������
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
