using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelsUpgradeSpawner : GenericUpgradeSpawner<WheelUpgrade>
{
    [SerializeField] private List<WheelSpawner> _wheelSpawners;

    private WheelSpawner _currentSpawner;

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
        }

        return true;
    }

    private WheelSpawner GetNextSpawner()
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