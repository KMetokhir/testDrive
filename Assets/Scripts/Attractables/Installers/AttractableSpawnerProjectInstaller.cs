using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class AttractableSpawnerProjectInstaller : MonoInstaller
{
    [SerializeField] AttractablesSpawner<Screw> spawnerPrefab;

    public override void InstallBindings()
    {
        AttractablesSpawner<Screw> spawnerGO = Instantiate(spawnerPrefab);
        SceneLoadHandler sceneLoadHandler = spawnerGO.GetComponentInChildren<SceneLoadHandler>();

        Container.Bind<SceneLoadHandler>()
            .FromInstance(sceneLoadHandler)
            .AsSingle()
            .NonLazy();

        Container.Bind<AttractablesSpawner<Screw>>()
           .FromInstance(spawnerGO)
           .AsSingle()          
           .NonLazy();     
    }
}
