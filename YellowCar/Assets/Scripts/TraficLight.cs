using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TraficLight : MonoBehaviour
{
    [SerializeField] private MeshRenderer  _meshRendererRed;
    [SerializeField] private MeshRenderer _meshRendererGreen;
    [SerializeField] private int _greenTimer, _redTimer;
    [SerializeField] private bool _isTimeToGreenLight;
    [SerializeField] private List <Spawner> _spawner;

    public Dictionary<int, NavMeshAgent> CarsListCrossRoad = new Dictionary<int, NavMeshAgent>();
    public bool IsTimeToGreenLight => _isTimeToGreenLight;
    public event Action<bool> GreenLightEvent;
    public Transform testObstacle;
    public ObstacleMove testMover;
    



    void Start()
    {
       if (_isTimeToGreenLight == true)
        {
            StartCoroutine(GreenLightCoroutine());
        }
       else
        {
            StartCoroutine(RedLightCoroutine());
        }
    }

    private IEnumerator GreenLightCoroutine()
    {
        _meshRendererRed.material.color = Color.black;
        _meshRendererGreen.material.color = Color.green;
        _isTimeToGreenLight = true;
        GreenLightEvent?.Invoke(true);
        foreach (var spawner in _spawner)
        {
            spawner.SwiftTraficLightAction(true);

        }
        RestartMachine(true);

        yield return new WaitForSeconds(_greenTimer);
        StartCoroutine(RedLightCoroutine());
    }

    private IEnumerator RedLightCoroutine()
    {
        if (testObstacle != null)
        {
            testMover.StartMoveObctacle(testObstacle, 5);
        }

        _meshRendererRed.material.color = Color.red;
        _meshRendererGreen.material.color = Color.black;
        _isTimeToGreenLight = false;
        GreenLightEvent?.Invoke(false);
        foreach (var spawner in _spawner)
        {
            spawner.SwiftTraficLightAction(false);

        }

        yield return new WaitForSeconds(_redTimer);
        StartCoroutine(GreenLightCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("триггер активировался");
        if (other.TryGetComponent(out Vehicle car) && !_isTimeToGreenLight)
        {
            car.NavMeshAgent.speed = 0;
            Debug.Log(car.Index);
            if (!CarsListCrossRoad.ContainsKey(car.Index))
                CarsListCrossRoad.Add(car.Index, car.NavMeshAgent);
        }
        else if (other.TryGetComponent(out Vehicle car2) && _isTimeToGreenLight)
        {
            car2.NavMeshAgent.speed = car2.Speed;
            Debug.Log(car.Index);
            if (!CarsListCrossRoad.ContainsKey(car2.Index))
            CarsListCrossRoad.Add(car2.Index, car2.NavMeshAgent);
        }

    }

    private void RestartMachine(bool canMove)
    {
        if (canMove)
        { //проходимся по всем машинкам, которые стоят на красном свете, и выставляем им заново скорость
            foreach (var car in CarsListCrossRoad)
            {

                if (car.Value.gameObject.TryGetComponent(out Vehicle carComponent))
                {
                    carComponent.GreenLightDetected = true;
                    car.Value.speed = carComponent.Speed;
                }
            }

        }
    }




}
