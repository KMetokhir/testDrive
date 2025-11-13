
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelBase : ObservableUpgradePart
{
    [SerializeField] private List<IWheelsSpawner> _spawners;
    [SerializeField] private List<WheelUpgradePart> _wheelPrefabs;

    public event Action<List<IWheel>, WheelBase> WheelsSpawned;

    private void Awake()
    {
        _spawners = GetComponents<IWheelsSpawner>().ToList();
    }

    /*public List<IWheel> GetWheels()
    {
        return _wheels;
    }*/

    private void Start()
    {
        List<IWheel> wheels = new List<IWheel>();

        foreach (WheelUpgradePart wheelPrefab in _wheelPrefabs)
        {
            for (int i = 0; i < wheelPrefab.Count; i++)
            {
                WheelUpgradePart upgrade = Instantiate(wheelPrefab);

                foreach (var spawner in _spawners)
                {
                    if (spawner.TrySpawn(upgrade))
                    {
                        wheels.Add(upgrade.Wheel);
                        break;
                    }
                }
            }
        }

        WheelsSpawned?.Invoke(wheels, this);
    }
}
