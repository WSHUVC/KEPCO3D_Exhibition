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


    // TargetController��  �̵���ų way�� startpoint, endPoint, way dierect �� ������
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
        // camera State ���� 
        CM_cameraState = CM_CameraState.Moving;

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
            yield return new WaitForSeconds(3);
            flag = graph.destinations[i];

            
        }
        // ���� �������� ���� ����������
        currentWayPoint = finalDestination;
        //Debug.Log("Target�� ���� ������ ����");
        yield return new WaitForSecondsRealtime(4f);
        FinishCameraMoving();
        cameraMoveEndEvent?.Invoke(button_Place);
        //Invoke("FinishCameraMoving", 4f);
    }

    private  void FinishCameraMoving()
    {
        CM_cameraState = CM_CameraState.None;
        Debug.Log("ī�޶� ���� �Ϸ�");
    }
    public void ZoomintoSensor(Tag_Sensor sensor)
    {
       
        //ZoomTrack ���� ����
        switch (sensor.myPlaceGroup)
        {
            case PlaceGroup._345kVGIS:
                Debug.Log("Sensor loacated in First Excute");
                //Firstzone Track���� Ʈ���� �ٲ��ش�. 
                TargetTracking_Camera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = FirstZoneTrack;
                currentWayPoint = 1;
                break;
            case PlaceGroup.�ֺ��б�:
                Debug.Log("Sensor located in Second Zone");
                currentWayPoint = 2;
                break;
            case PlaceGroup._765kVGIS: 
                currentWayPoint = 3;
                Debug.Log("Sensor located in ThirdZone");
                break;

 
        }
        // 3���� �ϳ� 
        // Target�� ���� ��ġ�� �̵�
        targetController.transform.position = sensor.transform.position;
    }
}
