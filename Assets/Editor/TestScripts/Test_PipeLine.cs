using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test_PipeLine : MonoBehaviour
{
    public List<Test_Pipe> pipes;

    private void Awake()
    {
        for(int i = 0; i < pipes.Count-1; ++i)
        {
            Debug.Log($"{pipes[i]}->{pipes[i + 1]}");
            pipes[i].next = pipes[i + 1];
            pipes[i].endEvent = pipes[i].NextAnimPlay;
        }
        pipes.Last().endEvent += ImLastPipe;
        pipes.First().Play();
    }



    void ImLastPipe()
    {
        foreach(var p in pipes)
        {
            p.SetOrigin();
        }
        pipes.First().Play();
        Debug.Log("LastPipeEnd");
    }
}
