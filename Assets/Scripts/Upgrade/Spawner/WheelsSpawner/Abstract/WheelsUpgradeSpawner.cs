using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using UnityEngine;

public class WheelsUpgradeSpawner<T,M> : GenericUpgradeSpawner<T>, IWheelsSpawner
    where T: WheelUpgrade 
    where M: WheelUpgradeSpawner<T>
{
    [SerializeField] private List<M> _wheelSpawners;

    private M _currentSpawner;

    public event Action<IWheelUpgrade> WheelSpawned;   

    private void OnValidate()
    {
        if (_wheelSpawners.Count <= 0)
        {
            throw new System.Exception("Wheel spawners not found");
        }
    }

    public override bool TrySpawn(UpgradePart part)
    {
        _currentSpawner = GetNextSpawner();

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

            WheelSpawned?.Invoke(part as T);
        }

        return true;
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