using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Timer : MonoBehaviour
{
    private EventBus _eventBus;
    private int _endTimer = 15;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    void Start()
    {
        _eventBus.DoubleScore += StartTimer;
        _eventBus.OnRestartTimer += RestartTimer;

    }


    private void StartTimer()
    {
        _endTimer = 15;
        StartCoroutine(TimerCourutine());
    }

    private IEnumerator TimerCourutine()
    {
       
        while(_endTimer > 0)
        {
            _endTimer--;
            _eventBus.OnTimerCount.Invoke(_endTimer);
            yield return new WaitForSeconds(1);
        }
        _eventBus.IsTimerActive = false;
    }
    private void RestartTimer()
    {
        _endTimer = 15;
    }
}
