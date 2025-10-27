using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WheelsUpgradeSpawner : GenericUpgradeSpawner<WheelUpgrade>
{
    [SerializeField] private List<WheelSpawner> _wheelSpawners;

    private List<WheelSpawner> _bookedSpawners;

    private void OnValidate()
    {
        if (_wheelSpawners.Count <= 0)
        {
            throw new System.Exception("Wheel spawners not found");
        }
    }

    private void Awake()
    {
        _bookedSpawners = new List<WheelSpawner>();
    }

    public override bool TrySpawn(UpgradePart part)
    {
        WheelSpawner spawner = GetNext();

        if (spawner.TrySpawn(part) == false)
        {
            return false;
        }
        else
        {
            Debug.Log(part.Count + " parts");
            Debug.Log(_wheelSpawners.Count + _bookedSpawners.Count);
            if (part.Count != _wheelSpawners.Count + _bookedSpawners.Count)
            {
                throw new System.Exception("Wheel spawners not enough");
            }
        }

        return true;
    }

    private WheelSpawner GetNext()
    {
        WheelSpawner first = _wheelSpawners.FirstOrDefault();

        if (first != null)
        {
            _wheelSpawners.Remove(first);
            _bookedSpawners.Add(first);

            return first;
        }
        else
        {
            first = _bookedSpawners.FirstOrDefault();
        }

        return first;
    }
}
