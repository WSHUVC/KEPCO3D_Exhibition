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

    }

    void Update()
    {

    }

    public void moveTargetAsync(Transform startPoint, Transform targetPoint, GameObject selectedway, bool Reverse)
    {
        nodesPositions.Clear();
        // �������� �߰�
        // �� �߰�
        nodes = selectedway.GetComponentsInChildren<Transform>();
        for (int i = 1; i < nodes.Length; i++) // �ڽ� ������Ʈ�� �̱����� 1���� 
        {
            nodesPositions.Add(nodes[i].position);
            //Debug.Log(nodesPositions[i]);
        }

        //���� ����(������/������)
        if (Reverse) nodesPositions.Reverse();
        // �������� �߰� 
        nodesPositions.Add(targetPoint.position);
        //������ �߰�
        nodesPositions.Insert(0, startPoint.position);
      
        foreach (var routePoint in nodes)
        {
            Debug.Log($" Beizer Move Order route -> {routePoint.name}");
        }
        // ���� ����
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
