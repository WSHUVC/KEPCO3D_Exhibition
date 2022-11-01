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
    public enum CM_CameraState
    {
        Moving,
        None,
    }

    public CM_CameraState CM_cameraState = CM_CameraState.None;


    // TargetController에  이동시킬 way의 startpoint, endPoint, way dierect 를 정해줌
    public int currentWayPoint = 0;
    public CinemachineVirtualCamera TargetTracking_Camera;
    public TargetController targetController;
    public Transform[] wayPoints;
    public GameObject[] ways;
    public CinemachineSmoothPath MainTrack;      // Paths
    public CinemachineSmoothPath FirstZoneTrack;
    public Tag_Sensor[] sensors; // Sensors

    private void Awake()
    {
        sensors = FindObjectOfType<Managers>().sensors;
    }

    public bool MoveTo(int index, Button button_Place= null)
    {
        if (CM_cameraState == CM_CameraState.Moving)
            return false;
        Debug.Log($"Move To {index} point");
        TargetTracking_Camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = MainTrack;
        StartCoroutine(MoveOrderTo(index, button_Place));
        return true;
    }

    public Action<Button> cameraMoveEndEvent;
    IEnumerator MoveOrderTo(int finalDestination, Button button_Place = null)
    {
        // camera State 무빙 
        CM_cameraState = CM_CameraState.Moving;

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
            yield return new WaitForSeconds(3);
            flag = graph.destinations[i];

            
        }
        // 최종 목적지를 현재 포지션으로
        currentWayPoint = finalDestination;
        //Debug.Log("Target이 최종 목적지 도착");
        yield return new WaitForSecondsRealtime(4f);
        FinishCameraMoving();
        cameraMoveEndEvent?.Invoke(button_Place);
        //Invoke("FinishCameraMoving", 4f);
    }

    private  void FinishCameraMoving()
    {
        CM_cameraState = CM_CameraState.None;
        Debug.Log("카메라 무빙 완료");
    }
    public void ZoomintoSensor(Tag_Sensor sensor)
    {
       
        //ZoomTrack 으로 변경
        switch (sensor.myPlaceGroup)
        {
            case PlaceGroup._345kVGIS:
                Debug.Log("Sensor loacated in First Excute");
                //Firstzone Track으로 트랙을 바꿔준다. 
                TargetTracking_Camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = FirstZoneTrack;
                currentWayPoint = 1;
                break;
            case PlaceGroup.주변압기:
                Debug.Log("Sensor located in Second Zone");
                currentWayPoint = 2;
                break;
            case PlaceGroup._765kVGIS: 
                currentWayPoint = 3;
                Debug.Log("Sensor located in ThirdZone");
                break;

 
        }
        // 3개중 하나 
        // Target을 센서 위치로 이동
        targetController.transform.position = sensor.transform.position;
    }
}
