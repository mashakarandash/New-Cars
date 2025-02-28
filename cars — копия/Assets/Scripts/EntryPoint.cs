using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private LevelGoal _levelGoal;
    [SerializeField] private ScriptableObjectPoolData _carPoolData;
    [SerializeField] private CointsChanger _coinsChanger;

    private ServiceLocator _serviceLocator;


    private EventBus _eventBus;
    private Storage _storage;
    
    void Awake()
    {
        _serviceLocator = new ServiceLocator();
        _carPoolData.AllCars.Clear();
        _carPoolData.Initialization();
        _eventBus = new EventBus();
        _storage = new Storage();
        _serviceLocator.RegisterService(_levelGoal);
        _serviceLocator.RegisterService(_eventBus);
        _serviceLocator.RegisterService(_carPoolData);
        _serviceLocator.RegisterService(_storage);
        _serviceLocator.RegisterService(_coinsChanger);
    }
    
    
}
