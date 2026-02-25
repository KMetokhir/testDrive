using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneLoadHandlerProjectInstaller : MonoInstaller
{
    [SerializeField] private SceneLoadHandler _sceneLoadHandlerPrefab;
    public override void InstallBindings()
    {
        Container.Bind<SceneLoadHandler>()
  .FromComponentInNewPrefab(_sceneLoadHandlerPrefab)
  .AsSingle()
  .NonLazy();

    }
}
