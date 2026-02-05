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
        _carConteiner = _carFactory.CreateCar(Vector3.zero);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_carConteiner != null)
            {
                Destroy(_carConteiner.GameObject());
            }

            SelectCar(0);
            SpawnCar();
        }
    }
}
