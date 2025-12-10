using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Trunk : MonoBehaviour // same as speed upgrader add trunk settings 
{
   // [SerializeField] private TrunkView _view;
    [SerializeField] private PowerUpgrader _upgrader;

    [SerializeField] private TrunkView _view;    

    [Inject]
    private void Construct(TrunkView view)
    {
        _view = view;
    }

    private uint _maxWeight;

    private uint _currentWeight;
    private List<ICollectable> _collectables;

    public event Action MaxWeightChanged;

    private void Awake()
    {
        _collectables = new List<ICollectable>();
    }

    private void OnEnable()
    {
        if (_upgrader != null) // tmp
        {
            _upgrader.UpgradeExecuted += SetNewStats;
        }
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
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

            _view.ShowValue(_currentWeight, (int)_maxWeight);

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

    private void SetNewStats(IPowerUpgradeData data)
    {
        _maxWeight = data.MaxWeight;
        _view.ShowValue(_currentWeight, (int)_maxWeight);

        MaxWeightChanged?.Invoke();
    }


    /*public class Factory : PlaceholderFactory<Trunk>
    {
    }*/
}
