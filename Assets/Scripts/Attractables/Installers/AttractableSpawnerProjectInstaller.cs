using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class AttractableSpawnerProjectInstaller : MonoInstaller
{
    [SerializeField] AttractablesSpawner spawnerPrefab;

    public override void InstallBindings()
    {
        AttractablesSpawner spawnerGO = Instantiate(spawnerPrefab);
        SceneLoadHandler sceneLoadHandler = spawnerGO.GetComponentInChildren<SceneLoadHandler>();

        Container.Bind<SceneLoadHandler>()
            .FromInstance(sceneLoadHandler)
            .AsSingle()
            .NonLazy();

        Container.Bind<AttractablesSpawner>()
           .FromInstance(spawnerGO)
           .AsSingle()          
           .NonLazy();     
    }
}
