using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class CarCompositDestroier : MonoBehaviour
{
    [SerializeField] List<IObservablePartSpawner> _spawners;

    private List<ObservableUpgradePart> _parts;

    private void Awake()
    {
        //spawnerd = GetComponentsInChildren<ObservablePartSpawner<WheelBase>>().ToList();
        _parts = new List<ObservableUpgradePart>();
        _spawners = GetComponentsInChildren<IObservablePartSpawner>().ToList();

        foreach (var spawner in _spawners)
        {
            spawner.PartSpawned += OnPartSpawned;

        }

        // Debug.LogError(_spawners.Count);
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.PartSpawned -= OnPartSpawned;

        }

        foreach (var part in _parts)
        {
            part.Destroied -= OnPartSpawned;
        }
    }

    private void OnPartSpawned(IUpgradePart part)
    {
        ObservableUpgradePart observabelPart = part as ObservableUpgradePart;

        if (observabelPart != null)
        {
            _parts.Add(observabelPart);
            observabelPart.Destroied += RemovePartFromList;
        }
    }

    private void RemovePartFromList(ObservableUpgradePart part)
    {
        _parts.Remove(part);
        part.Destroied -= RemovePartFromList;
        Debug.Log("remove from list");
    }

    public void Destroy()
    {
        foreach (var part in _parts)
        {
            part.Destroied -= RemovePartFromList;
            part.DestroyObject();
        }

        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    /*private void OnDestroy()
    {
        foreach(var part in _parts)
        {
            part.Destroied -= RemovePartFromList;
            part.DestroyObject();
        }
    }*/
}
