using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Elec"))
        {
            other.transform.GetComponent<MeshRenderer>().enabled = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Elec"))
        {
            other.transform.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
