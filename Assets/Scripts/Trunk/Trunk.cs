using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Trunk : MonoBehaviour
{
    [SerializeField] private TrunkView _view;

    private uint _currentWeight;
    private List<ICollectable> _collectables;

    private ITrunkData _data;

    public event Action MaxWeightChanged;

    [Inject]
    private void Construct(TrunkView view, ITrunkData data)
    {
        _view = view;
        _data = data;
    }

    private void Awake()
    {
        _collectables = new List<ICollectable>();
    }

    private void OnEnable()
    {
        _data.Changed += OnDataChanged;
    }

    private void OnDisable()
    {
        _data.Changed -= OnDataChanged;
    }

    public bool TryAdd(ICollectable collectable)
    {
        if (collectable == null)
        {
            return false;
        }

        if (collectable.Weight + _currentWeight <= _data.MaxWeight)
        {
            _collectables.Add(collectable);
            _currentWeight += collectable.Weight;

            _view.ShowValue(_currentWeight, (int)_data.MaxWeight);

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
        _view.ShowValue(_currentWeight, (int)_data.MaxWeight);

        return price;
    }

    private void OnDataChanged()
    {
        _view.ShowValue(_currentWeight, (int)_data.MaxWeight);
        MaxWeightChanged?.Invoke();
    }
}