using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private MagnetAria _magnetAria;
    [SerializeField] private MagnetField _magnetField;

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

    private void OnObjectsFound(List<IAttractable> list)
    {
        _magnetField.AddToField(list);

        foreach (IAttractable attractable in list)
        {
            attractable.Deactivate();
            _collectedObjects.Add(attractable);
        }
    }
}
