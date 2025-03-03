using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Furgon : Vehicle
{
    private EventBus _eventBus;


    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public override void OnMouseDown()
    {
        if (IsTemporaryYellowCar)
        {
            _eventBus.ScoreChanged.Invoke();
            gameObject.SetActive(false);
            return;
        }

        if (_eventBus.IsTimerActive == false)
        {
          _eventBus.DoubleScore.Invoke();
          gameObject.SetActive(false);
          _eventBus.IsTimerActive = true;
        }
        else
        {
            _eventBus.OnRestartTimer.Invoke();
            gameObject.SetActive(false);
        }
    }

    
}
