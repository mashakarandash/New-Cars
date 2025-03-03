using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTransfer : MonoBehaviour
{
    [SerializeField] private Transform _transformPoint;
    [SerializeField] private bool _canBoostSpeed;
    [SerializeField] private bool _shouldOvertake;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Vehicle"))
        {
            var vehicle = other.gameObject.GetComponent<Vehicle>();
            
            vehicle.NavMeshAgent.speed = 3;
            vehicle.NavMeshAgent.SetDestination(_transformPoint.position);
            if (_canBoostSpeed)
            StartCoroutine(WaitToBoostCoroutine(vehicle));
            
        }
    }

    private IEnumerator WaitToBoostCoroutine(Vehicle vehicle)
    {
        yield return new WaitForSeconds(2);
        
        vehicle.SetDestinationToPoint(_transformPoint.position, _shouldOvertake);
    }

}
