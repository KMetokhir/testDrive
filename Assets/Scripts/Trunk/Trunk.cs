using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    [SerializeField] private uint _maxWeight;
    [SerializeField] private TrunkView _view;

    private uint _currentWeight;
    private List<ICollectable> _collectables;

    private void Awake()
    {
        _collectables = new List<ICollectable>();
        _view.ShowValue(_currentWeight,  (int)_maxWeight);
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

            _view.ShowValue(_currentWeight,(int) _maxWeight);

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
        _view.ShowValue(_currentWeight, (int)_maxWeight);

        return price;
    }
}
