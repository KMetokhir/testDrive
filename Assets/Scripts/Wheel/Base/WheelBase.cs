using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelBase : CompositePart
{
    [SerializeField] private List<IWheelSpawnersContainer> _spawners;

    private List<WheelUpgradePart> _dependentUpgradeParts;

    public event Action<IWheel> WheelSpawned;
    public event Action<IWheel> WheelDestroied;

    private void Awake()
    {
        _spawners = GetComponents<IWheelSpawnersContainer>().ToList();
        _dependentUpgradeParts = new List<WheelUpgradePart>();

        foreach (var spawner in _spawners)
        {
            spawner.PartSpawned += OnWheelSpawned;
        }
    }

    public override List<UpgradePartSpawner> GetSpawners()
    {
        List<UpgradePartSpawner> spawners = new List<UpgradePartSpawner>();

        foreach (var spawner in _spawners)
        {
            UpgradePartSpawner upgradePartSpawner = spawner as UpgradePartSpawner;

            if (upgradePartSpawner != null)
            {
                spawners.Add(upgradePartSpawner);
            }
            else
            {
                throw new Exception($"Spawner {spawner} as UpgradePartSpawner == null");
            }
        }

        return spawners;
    }

    protected override void DestroyDependentParts()
    {
        foreach (var part in _dependentUpgradeParts)
        {
            part.Destroied -= OnWheelUpgradeDestroied;
            part.DestroyObject();
        }
    }

    private void OnWheelUpgradeDestroied(ObservableUpgradePart part)
    {
        WheelUpgradePart wheelPart = part as WheelUpgradePart;

        if (wheelPart != null)
        {
            WheelDestroied?.Invoke(wheelPart.Wheel);
            _dependentUpgradeParts.Remove(wheelPart);
        }
        else
        {
            throw new Exception("Incorrect input data " + nameof(part));
        }
    }

    private void OnWheelSpawned(WheelUpgradePart part)
    {
        part.transform.parent = transform.root;

        _dependentUpgradeParts.Add(part);

        part.Destroied += OnWheelUpgradeDestroied;

        WheelSpawned?.Invoke(part.Wheel);
    }
}