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
    // TargetController에  이동시킬 way의 startpoint, endPoint, way dierect 를 정해줌
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
        Debug.Log($"현재위치 ViewPoint :  {flag} ! 이동시작! ");
        bool reversed;
        int way = 0;

        // 최종 목적지까지 경로 생성
        graph.BFS(currentWayPoint, finalDestination);
        for (int i = 0; i < graph.destinations.Count; i++)
        {
            //방향선정
            reversed = graph.destinations[i] >= flag ? false : true;
            // way 선정 가는 방향이 길 번호
     
            way = graph.destinations[i] == 0 ? flag - 1: graph.destinations[i] - 1; 

            //Move
            Debug.Log($"WayPoint : {flag}로부터 Waypoint : {graph.destinations[i]}로 Way :{way}번 길을 {!reversed} 방향 시작 ");
            targetController.moveTargetAsync(wayPoints[flag], wayPoints[graph.destinations[i]], ways[way], reversed);
            Debug.Log($"WayPoint : {wayPoints[flag].name}로부터 Waypoint : {wayPoints[graph.destinations[i]].name}로 Way :{ways[way].name}번 길을 {!reversed} 방향 이동 완료 ");
            yield return new WaitForSeconds(5);
            flag = graph.destinations[i];
 
        }
        // 최종 목적지를 현재 포지션으로
        currentWayPoint = finalDestination;
    }
    public void ZoomintoSensor(int sensorNumber)
    {
        if (sensorNumber < 0)
            return;
        // ZoomTrack 으로 변경
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
        // 3개중 하나 
        // Target을 센서 위치로 이동
        targetController.moveToSensor(sensors[sensorNumber]);
    }
}
