
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _exitPortalPosition;
    [SerializeField] private Transform _newPositionToMove;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Vehicle car))
        {
            car.NavMeshAgent.Warp(_exitPortalPosition.position);
            car.transform.rotation = Quaternion.LookRotation(_newPositionToMove.position - car.transform.position); 
            car.NavMeshAgent.SetDestination(_newPositionToMove.position);
        }
    }

   
}
