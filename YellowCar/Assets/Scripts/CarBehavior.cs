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
        Debug.Log("заинжектилось");
        _eventBus = eventBus;
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
        if (IsTemporaryYellowCar)
        {
            _eventBus.ScoreChanged.Invoke();
            Debug.Log("добавление очков и преобразование ок");
        }
        
        else
        {
            _eventBus.MinusLifeAction.Invoke();
            Debug.Log("преобразование плохо");
        }
        gameObject.SetActive(false);

    }
}
