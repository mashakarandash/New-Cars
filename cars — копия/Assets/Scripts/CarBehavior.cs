using System.Collections;
using UnityEngine;

public class CarBehavior : Vehicle
{
    private EventBus _eventBus;
    [SerializeField] private bool _isTaxi;

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _eventBus = ServiceLocator.Instance.GetRegisterService<EventBus>();
    }

    public override void OnMouseDown()
    {
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
