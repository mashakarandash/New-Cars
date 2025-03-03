using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSide : MonoBehaviour
{
    [SerializeField] private Vehicle _vehicle;

    private void FixedUpdate()
    {

       /* Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 4, Color.red);

        if (Physics.Raycast(ray, 4))
        {
            _vehicle.RightIsFree = false;
        }
        else
        {
            _vehicle.RightIsFree = true;
        }*/

    }
}
