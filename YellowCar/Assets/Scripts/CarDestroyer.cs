using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    public LayerMask Layer;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Vehicle"))
        {
            other.gameObject.TryGetComponent(out Vehicle car);
            car.DoDestroy();
            
            
        }
    }
}
