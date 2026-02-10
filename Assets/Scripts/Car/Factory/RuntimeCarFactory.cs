using UnityEngine;
using Zenject;

public class RuntimeCarFactory 
{
    private readonly DiContainer _container;
    private CarConteiner _currentPrefab;
    private readonly SignalBus _signalBus;

    private CarConteiner CurrentCar { get;  set; } // not nessesary tmp

    [Inject]
    public RuntimeCarFactory(
        DiContainer container,
        SignalBus signalBus,
        CarConteiner defaultPrefab)
    {
        _container = container;
        _signalBus = signalBus;
        _currentPrefab = defaultPrefab;
    }

    public void SetCarPrefab(CarConteiner newPrefab)
    {
        _currentPrefab = newPrefab;
    }

    public CarConteiner CreateCar()
    {
        var car = _container.InstantiatePrefabForComponent<CarConteiner>(_currentPrefab);
        _container.Inject(car);

        CurrentCar = car;

        _signalBus.Fire(new CarSpawnedSignal(car)
        /*{
            Car = car
        }*/);       

        Debug.Log($"Created and rebound car: {car.name}");
        return car;
    }   
}
