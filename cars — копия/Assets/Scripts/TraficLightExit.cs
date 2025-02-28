using UnityEngine;

public class TraficLightExit : MonoBehaviour
{
    [SerializeField] private TraficLight _traficLight;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Vehicle car))
        {
            _traficLight.CarsListCrossRoad.Remove(car.Index);
        }
    }
}
