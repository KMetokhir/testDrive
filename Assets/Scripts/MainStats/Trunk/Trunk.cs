using System;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour // ame as speed upgrader
{
    [SerializeField] private TrunkView _view;
    [SerializeField] private PowerUpgrader _upgrader;

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
        _upgrader.UpgradeExecuted += SetNewStats;
        Debug.Log("IN Trunck");
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
}
