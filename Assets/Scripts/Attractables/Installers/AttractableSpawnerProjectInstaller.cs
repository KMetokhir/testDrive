using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class AttractableSpawnerProjectInstaller : MonoInstaller
{
    [SerializeField] AttractablesSpawner<Screw> _screwSpawnerPrefab;
    [SerializeField] AttractablesSpawner<Wrench> _wrenchSpawnerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<AttractablesSpawner<Screw>>()
    .FromComponentInNewPrefab(_screwSpawnerPrefab)
    .AsSingle()
    .NonLazy();

        Container.Bind<AttractablesSpawner<Wrench>>()
   .FromComponentInNewPrefab(_wrenchSpawnerPrefab)
   .AsSingle()
   .NonLazy();
    }
}
