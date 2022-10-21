using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CM_CameraManager : MonoBehaviour
{
    // TargetController에  이동시킬 way의 startpoint, endPoint, way dierect 를 정해줌

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
}

