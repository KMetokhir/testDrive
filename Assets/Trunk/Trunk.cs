using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [SerializeField] private uint _maxWeight;

    private uint _currentWeight;
    private List<ICollectable> _collectables;

    private void Awake()
    {
        _collectables = new List<ICollectable>();
    }

    public bool TryAdd(ICollectable collectable)
    {
        if (collectable == null)
        {
            return false;
        }

        if (collectable.Weight + _currentWeight <= _maxWeight)
        {
            _collectables.Add(collectable);
            _currentWeight += collectable.Weight;

            return true;
        }

        return false;
    }

    public uint GetSum()
    {
        uint price = 0;

        foreach (ICollectable item in _collectables)
        {
            price += item.Cost;
        }

        _collectables.Clear();
        _currentWeight = 0;

        return price;
    }
}
