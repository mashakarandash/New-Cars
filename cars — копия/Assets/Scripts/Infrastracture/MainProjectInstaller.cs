using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<Storage>()
            .FromNew()
            .AsSingle()
            .NonLazy();

    }
}
