using UnityEngine;
using Zenject;

public class RuntimeCarFactory 
{

    private readonly DiContainer _container;
    private CarConteiner _currentPrefab;

    [Inject]
    public RuntimeCarFactory(DiContainer container, CarConteiner defaultPrefab)
    {
        _container = container;
        _currentPrefab = defaultPrefab;
    }

    public void SetCarPrefab(CarConteiner newPrefab)
    {
        _currentPrefab = newPrefab;
    }

    public CarConteiner CreateCar(Vector3 position)
    {
        var car = _container.InstantiatePrefabForComponent<CarConteiner>(_currentPrefab);
        _container.Inject(car);
        car.transform.position = position;
        return car;
    }
}
