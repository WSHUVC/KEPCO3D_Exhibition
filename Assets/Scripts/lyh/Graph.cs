using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class Graph
{
    public int[] parent = new int[5]; // 어디서로부터 왔는지 
    public int[] distance = new int[5]; // 거리가 얼마나 되는지 
    public List<int> destinations = new List<int>();

    int[,] adj = new int[5,5]
    {
        {0, 1, 1, 1, 1}, // 1,2,3,4
        {1, 0, 0, 0, 0}, // 0
        {1, 0, 0, 0, 0}, // 0
        {1, 0, 0, 0, 0}, // 0
        {1, 0, 0, 0, 0}, // 0
    };

    public void BFS(int start, int targetPoint)
    {
        // bool[] found = new bool[7]; // 찾았는 지 여부
        // int[] parent = new int[7]; // 어디서로부터 왔는지 
        // int[] distance = new int[7]; // 거리가 얼마나 되는지 

        bool[] found = new bool[5]; // 찾았는 지 여부
        Queue<int> queue = new Queue<int>(); // 예약 목록, 선입 선출 
        queue.Enqueue(start);
        
        found[start] = true;
        parent[start] = start;
        distance[start] = 0;

        // 대기열에 하나라도 있으면 무한 루프
        while(queue.Count > 0)
        {
            int now = queue.Dequeue(); // 제일 오래기다리던 곳을 뽑아온다.  
            // 한번도 방문하지 않은 곳은 예약
            for (int next = 0; next < 5; next++)
            {
                if (adj[now, next] == 0) continue; // 인접하지 않았으면 스킵
                if (found[next]) continue; // 이미 발견한 곳이라면 스킵
                queue.Enqueue(next); // 예약 대기열에 추가
                found[next] = true; // 예약 체크 
                parent[next] = now;
                distance[next] = distance[now] + 1;
            }

        }

        /*
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"뷰포인트 {i}는 {parent[i]}가 부모노드 이고 시작점 {start}로부터 {distance[i]} 만큼 떨어져 있다.");
        }
        */



        /// Generate Route
        destinations.Add(targetPoint);

        while (!destinations.Contains(start))
        {
            if (targetPoint != start)
                destinations.Add(parent[targetPoint]);

            targetPoint = parent[targetPoint];
        }
        destinations.RemoveAt(destinations.Count - 1);
        destinations.Reverse();
    }


    
    

    // 경로 길 찾기 
    

}
