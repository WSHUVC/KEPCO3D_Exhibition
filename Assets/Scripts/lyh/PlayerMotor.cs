using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;

    public  Transform destination;


    private void Update()
    {
                
    }

    private void Start()
    {
    
    }

    private void LateUpdate()
    {
        LerpMoveToZoneNumber();
    }
    public void LerpMoveToZoneNumber()
    {
        transform.position = Vector3.Lerp(transform.position, destination.position, Time.deltaTime * 1/2);
    }



    private void SwitchDestination(int num)
    {
         
    }

}

