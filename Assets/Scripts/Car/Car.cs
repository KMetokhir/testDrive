using System;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, ISeller, ICarLevel /*, ILevel*/
{
    [SerializeField] private Magnet _magnet;
    [SerializeField] private Trunk _trunk;
    [SerializeField] private Money _money;

    /* private ILevel _level;*/
    public uint Level { get; private set; }
   

    private void Start()
    {
        _magnet.StartWorke();
        Level = 1;
    }

    private void OnEnable()
    {
        _magnet.ObjectInMagnetAria += OnObjectInMagnetAria;
        _trunk.MaxWeightChanged += OnMaxWeightChanged;
    }   

    private void OnDisable()
    {
        _magnet.ObjectInMagnetAria -= OnObjectInMagnetAria;
        _trunk.MaxWeightChanged -= OnMaxWeightChanged;
    }

    private void OnMaxWeightChanged()
    {        
        _magnet.Stop();
        _magnet.StartWorke();
    }

    private void OnObjectInMagnetAria(ICollectable collectable)
    {
        if (_trunk.TryAdd(collectable) == false)
        {
            _magnet.Stop();
        }
    }

    public List<IAttractable> Buy()
    {
        uint money = _trunk.GetSum();
        _money.Increase(money);        

        return _magnet.GetAttractedObjects();
    }
}
