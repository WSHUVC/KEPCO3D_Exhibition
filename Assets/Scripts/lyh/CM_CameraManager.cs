using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using WSH.UI;
using WSH.Core;
using System.Linq;
using UnityEngine.UI;
using System;

public class CM_CameraManager : MonoBehaviour
{
    // TargetController��  �̵���ų way�� startpoint, endPoint, way dierect �� ������
    public int currentWayPoint = 0;
    public CinemachineVirtualCamera TargetTracking_Camera;
    public TargetController targetController;
    public Transform[] wayPoints;
    public GameObject[] ways;

    // Paths
    public CinemachineSmoothPath MainTrack;
    public CinemachineSmoothPath FirstZoneTrack;
    //public CinemachineSmoothPath SecondZoneTrack;
    //public CinemachineSmoothPath ThirdZoneTrack;

    // Sensors
    public Transform[] sensors = new Transform[12];

    public static CM_CameraManager instance;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public void MoveTo(int index)
    {
        Debug.Log($"Move To {index} point");
        TargetTracking_Camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
        StartCoroutine(MoveOrderTo(index));
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
    public void ZoomintoSensor(int sensorNumber)
    {
        if (sensorNumber < 0)
            return;
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
