using UnityEngine;
using Zenject;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private CarConteiner[] _carPrefabs;

    [SerializeField] private CarConteiner _carConteiner;

    private RuntimeCarFactory _carFactory;

    [Inject]
    private void Construct(RuntimeCarFactory carFactory)
    {
        _carFactory = carFactory;
    }

    // Test with right mouse button
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectCar(0);
            SpawnCar();
        }       

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectCar(1);
            SpawnCar();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectCar(1);
            SpawnCar();
        }
    }

    public void SelectCar(int index)
    {
        if (index >= 0 && index < _carPrefabs.Length)
        {
            _carFactory.SetCarPrefab(_carPrefabs[index]);           
        }
    }

    public void SpawnCar()
    {
        if (_carConteiner != null)
        {
            _carConteiner.Destroy();           
            _carConteiner = null;
        }
    
        _carConteiner = _carFactory.CreateCar();
        _carConteiner.transform.parent = null;      
    }
}