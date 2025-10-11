using System;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private MagnetAria _magnetAria;
    [SerializeField] private MagnetField _magnetField;

    private bool _isWork;

    public event Action<ICollectable> ObjectInMagnetAria;

    public List<IAttractable> _collectedObjects;

    private void Awake()
    {
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
        List<IAttractable> obgects = new List<IAttractable> (_collectedObjects);
        _collectedObjects.Clear();
        _magnetField.Clear();

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

                item.Deactivate();
                _collectedObjects.Add(item);
            }
            else
            {
                return;
            }
        }
    }
}
