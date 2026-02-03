using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;
using Zenject;

public class SeneInstaller : MonoInstaller
{

    //[SerializeField] private AttractablesSpawner _spawner;
    //[SerializeField] SpawnerLoader spawnerLoader;

    /*private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Container.Rebind<AttractablesSpawner>()
         .FromInstance(_spawner);
    }*/

    public override void InstallBindings()
    {

       /* Container.Bind<SpawnerLoader>()
           .FromInstance(spawnerLoader)
           .AsSingle()
           .NonLazy();*/
    }
}