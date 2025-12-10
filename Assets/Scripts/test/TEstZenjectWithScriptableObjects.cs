using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class TEstZenjectWithScriptableObjects : MonoInstaller
{
    [SerializeField] TestSO _So;

    public override void InstallBindings()
    {
        Container.BindInstance(_So).AsSingle();
    }
}
