using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class CarPartsDestroier : MonoBehaviour
{
    [SerializeField] List<IObservablePartSpawner> spawnerd;

    private List<IUpgradePart> _parts;

    private void Awake()
    {
        //spawnerd = GetComponentsInChildren<ObservablePartSpawner<WheelBase>>().ToList();
        var spawners = GetComponentsInChildren<IObservablePartSpawner>();

        foreach (var spawner in spawners)
        {
            spawner.PartSpawned += OnPartSpawned;
        }
    }

    private void OnPartSpawned(IUpgradePart part)
    {
       
    }
}
