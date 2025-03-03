using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private LevelGoal _levelGoal;
    [SerializeField] private ScriptableObjectPoolData _carPoolData;
    [SerializeField] private CointsChanger _coinsChanger;

    public override void InstallBindings()
    {
        BindLevelGoal();
        BindScriptableObjectPoolData();
        BindCoinsChanger();
    }

    public override void Start()
    {
        _carPoolData.AllCars.Clear();
        _carPoolData.ConstractPool(Container.Resolve<DiContainer>());
    }

    private void BindLevelGoal()
    {
        Container
             .Bind<LevelGoal>()
             .FromInstance(_levelGoal)
             .AsSingle()
             .NonLazy();
    }

    private void BindScriptableObjectPoolData()
    {
        Container
            .Bind<ScriptableObjectPoolData>()
            .FromInstance(_carPoolData)
            .AsSingle()
            .NonLazy();
    }

    private void BindCoinsChanger()
    {
        Container
            .Bind<CointsChanger>()
            .FromInstance(_coinsChanger)
            .AsSingle()
            .NonLazy();
    }
}
