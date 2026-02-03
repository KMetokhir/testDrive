using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    /* [Header("Persistent Object Settings")]
     [SerializeField] private string persistentObjectName = "GlobalPersistentObject";
     [SerializeField] private bool debugLogs = true;*/

    [SerializeField] AttractablesSpawner spawnerPrefab;

    public override void InstallBindings()
    {
        AttractablesSpawner spawnerGO = Instantiate(spawnerPrefab);


        /*Container.BindInterfacesAndSelfTo<AttractablesSpawner>()
           .FromComponentInNewPrefab(spawnerGO)
           .AsSingle()
           .NonLazy();*/

        Container.Bind<AttractablesSpawner>()
           .FromInstance(spawnerGO)
           .AsSingle()          // Only one instance ever
           .NonLazy();

        /*if (debugLogs)
            Debug.Log("[ProjectContext] Installing PersistentObject...");

        // Create the PersistentObject GameObject
        GameObject persistentGO = new GameObject(persistentObjectName);

        // Add the PersistentObject component
        PersistentObject persistentObject = persistentGO.AddComponent<PersistentObject>();

        // Configure it (optional)
        persistentObject.Initialize("Global_Instance");

        // Mark as DontDestroyOnLoad
        DontDestroyOnLoad(persistentGO);

        // BIND IT GLOBALLY - This is the key!
        Container.Bind<PersistentObject>()
            .FromInstance(persistentObject)
            .AsSingle()          // Only one instance ever
            .NonLazy();          // Create immediately

        // Also bind as interfaces if needed
        Container.BindInterfacesTo<PersistentObject>()
            .FromInstance(persistentObject)
            .AsSingle();

        if (debugLogs)
            Debug.Log($"[ProjectContext] PersistentObject '{persistentObjectName}' created and bound globally");*/
    }
}
