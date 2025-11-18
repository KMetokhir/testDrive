
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class WheelBase : CompositePart
{

    [SerializeField] private List<WheelsUpgradeSpawner> _spawners;
   // [SerializeField] private List<WheelUpgradePart> _wheelPrefabs;

    private List<WheelUpgradePart> _dependentUpgradeParts;

    public event Action<IWheel> WheelSpawned;
    public event Action<IWheel> WheelDestroied;    

    private void Awake()
    {
        _spawners = GetComponents<WheelsUpgradeSpawner>().ToList();
        _dependentUpgradeParts = new List<WheelUpgradePart>();

        foreach (var spawner in _spawners)
        {
            spawner.WheelSpawned += OnWheelSpawned;
        }
    }
    private void Start()
    {
        

        /*WheelBase basew = this;

        UpgradePart part = basew as UpgradePart;

        part.DestroyObject();*/

        /*foreach (WheelUpgradePart wheelPrefab in _wheelPrefabs)
    {
        for (int i = 0; i < wheelPrefab.Count; i++)
        {
            WheelUpgradePart upgrade = Instantiate(wheelPrefab);

            foreach (var spawner in _spawners)
            {
                spawner.WheelSpawned += OnWheelSpawned;

                if (spawner.TrySpawn(upgrade))
                {

                    _dependentUpgradeParts.Add(upgrade);

                    upgrade.Destroied += OnWheelUpgradeDestroied;

                    break;
                }
            }
        }
    }        */
    }

    public override List<UpgradePartSpawner> GetSpawners()
    {
        List<UpgradePartSpawner> spawners = new List<UpgradePartSpawner>();

        foreach (var spawner in _spawners)
        {
            spawners.Add(spawner);
        }

        return spawners;
    }

    protected override void DestroyDependentParts()/// invoke that wheel destroid
    { 
        foreach (var part in _dependentUpgradeParts)
        {
            part.Destroied -= OnWheelUpgradeDestroied;
            part.DestroyObject();
        }
    }

    /*public List<IWheel> GetWheels()
    {
        return _wheels;
    }*/

    

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
        _dependentUpgradeParts.Add(part);

        part.Destroied += OnWheelUpgradeDestroied;

        WheelSpawned?.Invoke(part.Wheel);

      
    }
}
