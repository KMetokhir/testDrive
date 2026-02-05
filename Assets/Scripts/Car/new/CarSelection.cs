using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private CarConteiner[] _carPrefabs;

    private RuntimeCarFactory _carFactory;

    [SerializeField] private CarConteiner _carConteiner;  

    [Inject]
    private void Construct(RuntimeCarFactory carFactory)
    {
        _carFactory = carFactory;
    }

    // Call this from UI buttons
    public void SelectCar(int index)
    {
        if (index >= 0 && index < _carPrefabs.Length)
        {
            _carFactory.SetCarPrefab(_carPrefabs[index]);
            Debug.Log($"Selected car: {_carPrefabs[index].name}");
        }
    }

    public void SpawnCar()
    {
        // Destroy previous car if exists
        if (_carConteiner != null)
        {
            Destroy(_carConteiner.gameObject);
            _carConteiner = null;
        }

        // Create new car
        _carConteiner = _carFactory.CreateCar();
        Debug.Log($"Spawned car: {_carConteiner.name}");
    }

    // Test with right mouse button
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectCar(0);
            SpawnCar();
        }

        // Test with other keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectCar(0);
            SpawnCar();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectCar(1);
            SpawnCar();
        }
    }
}
