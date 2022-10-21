using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_CameraManager : MonoBehaviour
{
    // TargetController��  �̵���ų way�� startpoint, endPoint, way dierect �� ������

    private int currentWayPoint = 0;

    public TargetController targetController;
    public static CM_CameraManager instance;
    public Transform[] wayPoints;
    public GameObject[] ways;


    public GameObject TargetTracking_Camera;


    
    private void Awake()
    {

        if (instance != null) return;
        instance = this;

    }

    public void moveTopToFristZone()
    {
        MoveCameraTo(1);
    }

    public void moveTopToSecondZone()
    {
        MoveCameraTo(2);
    }


    public void moveTopToThirdZone()
    {
      MoveCameraTo(3);
    }

    public void moveTopToFourthZone()
    {
        MoveCameraTo(4);
    }



    public void MoveOrder(int finalDestination)
    {
        Graph graph = new Graph();
        int flag = currentWayPoint;
        bool reversed;
        int way = 0;

        // ���� ���������� ��� ����
        graph.BFS(currentWayPoint, finalDestination);
        graph.destinations.ForEach(destination =>
        {
            // Todo -> moveTarget ���� 

            // ���� ���� (�̵��� destnation�� ���� flag���� ũ�ų� ���ٸ� ������ �ƴϸ� ������) 
            reversed = destination >= flag ? false : true;


            // way ���� ���� ������ �� ��ȣ
            way = destination;

            // ��ĭ �̵� !!
            Debug.Log($"{flag}�κ���{destination}�� {way}�� ���� {!reversed} �������� ����.");
            targetController.moveTargetAsync(wayPoints[flag], wayPoints[destination], ways[way], reversed);
            flag = destination;

        });
        currentWayPoint = finalDestination;

    }



    IEnumerator MoveOrderTo(int finalDestination)
    {
        Graph graph = new Graph();
        int flag = currentWayPoint;
        Debug.Log($"������ġ ViewPoint :  {flag} ! �̵�����! ");
        bool reversed;
        int way = 0;

        // ���� ���������� ��� ����
        graph.BFS(currentWayPoint, finalDestination);
        for (int i = 0; i < graph.destinations.Count; i++)
        {
            //���⼱��
            reversed = graph.destinations[i] >= flag ? false : true;
            // way ���� ���� ������ �� ��ȣ

     
            way = graph.destinations[i] == 0 ? flag - 1: graph.destinations[i] - 1; 


            //Move
            Debug.Log($"WayPoint : {flag}�κ��� Waypoint : {graph.destinations[i]}�� Way :{way}�� ���� {!reversed} ���� ���� ");
            targetController.moveTargetAsync(wayPoints[flag], wayPoints[graph.destinations[i]], ways[way], reversed);
            Debug.Log($"WayPoint : {wayPoints[flag].name}�κ��� Waypoint : {wayPoints[graph.destinations[i]].name}�� Way :{ways[way].name}�� ���� {!reversed} ���� �̵� �Ϸ� ");
            yield return new WaitForSeconds(5);
            flag = graph.destinations[i];
 
        }
        // ���� �������� ���� ����������
        currentWayPoint = finalDestination;
    }

    public void MoveCameraTo(int viewPoint)
    {
        StartCoroutine(MoveOrderTo(viewPoint));

    }
}

