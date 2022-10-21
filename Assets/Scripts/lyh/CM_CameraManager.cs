using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_CameraManager : MonoBehaviour
{
    // TargetController��  �̵���ų way�� startpoint, endPoint, way dierect �� ������

    public int currentWayPoint = 0;


    public CinemachineVirtualCamera TargetTracking_Camera;

    public TargetController targetController;
    public static CM_CameraManager instance;
    public Transform[] wayPoints;
    public GameObject[] ways;

    // Paths
    public CinemachineSmoothPath MainTrack;
    public CinemachineSmoothPath FirstZoneTrack;
    public CinemachineSmoothPath SecondZoneTrack;
    public CinemachineSmoothPath ThirdZoneTrack;

    // Sensors
    public Transform[] sensors = new Transform[12];

    private void Awake()
    {

        if (instance != null) return;
        instance = this;

    }

    public void moveTopToFristZone()
    {
        TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
        MoveCameraTo(1);
    }

    public void moveTopToSecondZone()
    {
        TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
        MoveCameraTo(2);
    }


    public void moveTopToThirdZone()
    {
        TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
        MoveCameraTo(3);
    }

    public void moveTopToFourthZone()
    {
        TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
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




    public void ChangeDollyTrack()
    {
        TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = FirstZoneTrack;
    }



    public void  ZoomintoSensor(int sensorNumber)
    {
        
        // ZoomTrack ���� ����
        switch (sensorNumber)
        {
            case int n when (n < 4):
                Debug.Log("FirstZone Excute");
                currentWayPoint = 1;
                //TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = FirstZoneTrack;
                break;

            case int n when (n < 8 && n >= 4):
                //TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = SecondZoneTrack;
                currentWayPoint = 2;
                Debug.Log("SecondZone Excute");
                break;

            case int n when (12 < 8 && n >= 8):
                //TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = ThirdZoneTrack;
                currentWayPoint = 3;
                Debug.Log("ThirdZone Excute");
                break;
        }
        



        // 3���� �ϳ� 
        // Target�� ���� ��ġ�� �̵�
        targetController.moveToSensor(sensors[sensorNumber]);
    }


}

