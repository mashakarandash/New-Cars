using UnityEngine;
using UnityEngine.AI;

public class TraficLightEnter : MonoBehaviour
{
    [SerializeField] private TraficLight _traficLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent car))
        {
            car.speed = 6;
        }

        if (other.TryGetComponent(out Vehicle car2) && !_traficLight.IsTimeToGreenLight)
        {
            car2.GreenLightDetected = false;
            if(!_traficLight.CarsListCrossRoad.ContainsKey(car2.Index))
            _traficLight.CarsListCrossRoad.Add(car2.Index, car);
        }
    }
    
}
