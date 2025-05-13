using System.Collections;
using UnityEngine;
using Zenject;

public class CarBehavior : Vehicle
{
    private EventBus _eventBus;
    [SerializeField] private bool _isTaxi;

    [Inject]
    private void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        if (IsTemporaryYellowCar)
        {
            _eventBus.ScoreChanged.Invoke();
        }
        
        else
        {
            _eventBus.MinusLifeAction.Invoke();
        }
        gameObject.SetActive(false);

    }
}
