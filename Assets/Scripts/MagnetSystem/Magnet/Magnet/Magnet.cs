using System;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : ObservableUpgradePart, IConnectable
{
    [SerializeField] private MagnetAria _magnetAria;
    [SerializeField] private MagnetField _magnetField;

    [SerializeField] private ConnectionPoint _connectionPoint;

    [SerializeField] private bool _isWork;  

    private List<IAttractable> _collectedObjects;

    public event Action<ICollectable> ObjectInMagnetAria; 

    public ConnectionPoint Point => _connectionPoint;
    public Rigidbody ConnectionRigidbody {get; private set;}

    private void Awake()
    {
        ConnectionRigidbody = GetComponent<Rigidbody>();
        _collectedObjects = new List<IAttractable>();
    }

    private void OnEnable()
    {
        _magnetAria.AttractableObjectsFound += OnObjectsFound;
    }

    private void OnDisable()
    {
        _magnetAria.AttractableObjectsFound -= OnObjectsFound;
    }

    public void StartWorke()
    {
        _isWork = true;
        _magnetAria.StartDetecting();
    }

    public void Stop()
    {
        _isWork = false;
        _magnetAria.StopDetecting();
    }

    public List<IAttractable> GetAttractedObjects()
    {
        List<IAttractable> obgects = new List<IAttractable>(_collectedObjects);
        _collectedObjects.Clear();
        _magnetField.Clear();

        StartWorke();

        return obgects;
    }

    private void OnObjectsFound(List<IAttractable> list)
    {
        foreach (IAttractable item in list)
        {
            ObjectInMagnetAria?.Invoke(item);

            if (_isWork)
            {
                _magnetField.AddToField(item);

                item.Collect();
                _collectedObjects.Add(item);
            }
            else
            {
                return;
            }
        }
    }
}