using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindStorage();
        EventBus();
        BindMasterSave();
    }

    private void EventBus()
    {
        Container
                    .Bind<EventBus>()
                    .FromNew()
                    .AsSingle()
                    .NonLazy();
    }

    private void BindStorage()
    {
        Container
            .Bind<Storage>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void BindMasterSave()
    {
        Container
            .Bind<MasterSave>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}
