using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private int _scoreToGrow;
    [SerializeField] private ScriptableObjectPoolData _carPoolData;
   // [SerializeField] private CointsChanger _coinsChanger;
    [SerializeField] private AudioSource _audioEffects;
    [SerializeField] private GameStarter _gameStarter;

    public override void InstallBindings()
    {
        BindScoreHolder();

        BindLevelGoal();
        BindScriptableObjectPoolData();
        //  BindCoinsChanger();
        BindAudioEffects();
        BindGameStarter();
        GameObject canvas = Container.InstantiatePrefabResource("Prefabs/Canvas");
        Container.QueueForInject(canvas);
    }

    private void BindScoreHolder()
    {
        Container
            .Bind<ScoreHolder>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void Awake()
    {
        Container.Resolve<MasterSave>().LoadAllData();
    }

    public override void Start()
    {
        _carPoolData.AllCars.Clear();
        _carPoolData.ConstractPool(Container.Resolve<DiContainer>());
        
    }

    private void BindLevelGoal()
    {
        Container
             .Bind<int>()
             .WithId("ScoreToGrow")
             .FromInstance(_scoreToGrow)
             .AsCached()
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

 /*   private void BindCoinsChanger()
    {
        Container
            .Bind<CointsChanger>()
            .FromInstance(_coinsChanger)
            .AsSingle()
            .NonLazy();
    }*/

    private void BindAudioEffects()
    {
        Container
            .Bind<AudioSource>()
            .WithId("Effects")
             .FromInstance(_audioEffects)
             .AsCached()
             .NonLazy();
    }

   private void BindGameStarter()
    {
        Container
             .Bind<GameStarter>()
             .FromInstance(_gameStarter)
             .AsSingle()
             .NonLazy();
    }
}
