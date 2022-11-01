using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ElecTrigger : MonoBehaviour
{
    Dictionary<Collider, MeshRenderer> electricTable = new Dictionary<Collider, MeshRenderer>();
    Animator actor;
    private void Awake()
    {
        var elecs = FindObjectsOfType<Collider>()
            .Where(e => e.tag.Equals("Elec"))
            .Select(e=>e.GetComponent<Collider>())
            .ToArray();
        foreach(var e in elecs)
        {
            electricTable.Add(e, e.GetComponent<MeshRenderer>());
        }

        actor = GetComponent<Animator>();
    }

    public void Play()
    {
        GetComponent<Collider>().enabled = true;
        actor.SetBool("Play", true);
    }

    public void Stop()
    {
        foreach (var mesh in activeMeshList)
        {
            mesh.enabled = false;
        }
        activeMeshList.Clear();
        actor.SetBool("Play", false);
        GetComponent<Collider>().enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Play();
        if (Input.GetKeyDown(KeyCode.W))
            Stop();
    }

    List<MeshRenderer> activeMeshList = new List<MeshRenderer>();
    private void OnTriggerEnter(Collider other)
    {
        if (electricTable.TryGetValue(other, out var mesh))
        {
            mesh.enabled = true;
            activeMeshList.Add(mesh);
        }
        //if (other.tag.Equals("Elec"))
        //{
        //    other.transform.GetComponent<MeshRenderer>().enabled = true;
        //}
    }


    private void OnTriggerExit(Collider other)
    {
        if (electricTable.TryGetValue(other, out var mesh))
            mesh.enabled = false;

        //if (other.tag.Equals("Elec"))
        //{
        //    other.transform.GetComponent<MeshRenderer>().enabled = false;
        //}
    }
}
