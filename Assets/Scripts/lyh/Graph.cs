using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class Graph
{
    public int[] parent = new int[5]; // ��𼭷κ��� �Դ��� 
    public int[] distance = new int[5]; // �Ÿ��� �󸶳� �Ǵ��� 
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
        // bool[] found = new bool[7]; // ã�Ҵ� �� ����
        // int[] parent = new int[7]; // ��𼭷κ��� �Դ��� 
        // int[] distance = new int[7]; // �Ÿ��� �󸶳� �Ǵ��� 

        bool[] found = new bool[5]; // ã�Ҵ� �� ����
        Queue<int> queue = new Queue<int>(); // ���� ���, ���� ���� 
        queue.Enqueue(start);
        
        found[start] = true;
        parent[start] = start;
        distance[start] = 0;

        // ��⿭�� �ϳ��� ������ ���� ����
        while(queue.Count > 0)
        {
            int now = queue.Dequeue(); // ���� ������ٸ��� ���� �̾ƿ´�.  
            // �ѹ��� �湮���� ���� ���� ����
            for (int next = 0; next < 5; next++)
            {
                if (adj[now, next] == 0) continue; // �������� �ʾ����� ��ŵ
                if (found[next]) continue; // �̹� �߰��� ���̶�� ��ŵ
                queue.Enqueue(next); // ���� ��⿭�� �߰�
                found[next] = true; // ���� üũ 
                parent[next] = now;
                distance[next] = distance[now] + 1;
            }

        }

        /*
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"������Ʈ {i}�� {parent[i]}�� �θ��� �̰� ������ {start}�κ��� {distance[i]} ��ŭ ������ �ִ�.");
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


    
    

    // ��� �� ã�� 
    

}
