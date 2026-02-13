using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenericSpawnersContainer<T, M> : GenericObservableSpawner<T>
   where T : UpgradePart
    where M : GenericUpgradeSpawner<T>
{
    [SerializeField] private  List<M> _wheelSpawners;

    private M _currentSpawner;

    public override UpgradePart LastSpawnedPart => _currentSpawner.LastSpawnedPart;

    public override event Action<T> PartSpawned;

    private void OnValidate()
    {
        if (_wheelSpawners.Count <= 0)
        {
            throw new System.Exception("Wheel spawners not found");
        }
    }

    public override bool TrySpawn(UpgradePart part)
    {
        if (_currentSpawner == null)
        {
            _currentSpawner = GetNextSpawner();
        }

        if (_currentSpawner.TrySpawn(part) == false)
        {
            return false;
        }
        else
        {
            if (part.Count > _wheelSpawners.Count)
            {
                throw new System.Exception("Wheel spawners not enough");
            }
            
            PartSpawned?.Invoke(part as T);
        }       

        _currentSpawner = GetNextSpawner();

        return true;
    }

    public override bool IsSpawnPossible(UpgradePart part)
    {
        M spawner = GetNextSpawner();

        return spawner.IsSpawnPossible(part);
    }

    private M GetNextSpawner()
    {
        if (_currentSpawner == null)
        {
            _currentSpawner = _wheelSpawners.FirstOrDefault();

            return _currentSpawner;
        }

        int index = _wheelSpawners.IndexOf(_currentSpawner);

        if (index == -1)
            throw new ArgumentException("Element not found in list");

        int nextIndex = (index + 1) % _wheelSpawners.Count;

        return _wheelSpawners[nextIndex];
    }
}