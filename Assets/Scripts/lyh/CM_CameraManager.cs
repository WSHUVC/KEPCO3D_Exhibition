using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using WSH.UI;
using WSH.Core;
using System.Linq;
using UnityEngine.UI;

public class CM_CameraManager : MonoBehaviour
{
    // TargetController에  이동시킬 way의 startpoint, endPoint, way dierect 를 정해줌

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

        var panels = FindObjectOfType<UI_Panel_BottomButtons>().panel_PlaceAndSensors;

        for(int i = 0; i < panels.Length; ++i)
        {
            int index = i+1;
            panels[i].button_Place.onClick.AddListener(()=>MoveToIndexPoint(index));

            //int c = 1;
            foreach(var sensor in panels[i].panel_PlaceSensorList.button_Sensors)
            {
                sensor.GetComponent<Button>().onClick.AddListener(()=>ZoomintoSensor(sensor.index));
            }
        }

        var manager = FindObjectOfType<Managers>();
        foreach(var f in manager.placeFlags)
        {
            f.button_MoveToPoint.onClick.AddListener(() => MoveToIndexPoint(f.index));
        }

        foreach(var f in manager.sensorFlags)
        {
            f.button_MoveToPoint.onClick.AddListener(() => ZoomintoSensor(f.index));
        }
    }
    public void MoveToIndexPoint(int index)
    {
        Debug.Log($"Move To {index} point");
        TargetTracking_Camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
        MoveCameraTo(index);
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

        // 최종 목적지까지 경로 생성
        graph.BFS(currentWayPoint, finalDestination);
        graph.destinations.ForEach(destination =>
        {
            // Todo -> moveTarget 실행 

            // 방향 선정 (이동할 destnation가 현재 flag보다 크거나 같다면 순방향 아니면 역방향) 
            reversed = destination >= flag ? false : true;


            // way 선정 가는 방향이 길 번호
            way = destination;

            // 한칸 이동 !!
            Debug.Log($"{flag}로부터{destination}로 {way}번 길을 {!reversed} 방향으로 간다.");
            targetController.moveTargetAsync(wayPoints[flag], wayPoints[destination], ways[way], reversed);
            flag = destination;

        });
        currentWayPoint = finalDestination;

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

