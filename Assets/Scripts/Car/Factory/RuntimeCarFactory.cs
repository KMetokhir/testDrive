using UnityEngine;
using Zenject;

public class RuntimeCarFactory 
{
    private readonly DiContainer _container;
    private CarConteiner _currentPrefab;
    private readonly SignalBus _signalBus;

    private CarConteiner CurrentCar { get;  set; } // not nessesary tmp

    private MagnetSetingsProvider _magnetSetingsProvider;    

    [Inject]
    public RuntimeCarFactory(
        DiContainer container,
        SignalBus signalBus,
        CarConteiner defaultPrefab,
        MagnetSetingsProvider magnetSetingsProvider)
    {
        _container = container;
        _signalBus = signalBus;
        _currentPrefab = defaultPrefab;
        _magnetSetingsProvider = magnetSetingsProvider;
    }

    public void SetCarPrefab(CarConteiner newPrefab)
    {
        _currentPrefab = newPrefab;
    }

    public CarConteiner CreateCar()
    {
        var car = _container.InstantiatePrefabForComponent<CarConteiner>(_currentPrefab);       

        CurrentCar = car;

        IMagnetUpgradeData magnetData = car.GetComponent<IMagnetUpgradeData>();

        if (magnetData == null)
        {
            Debug.LogError($"Car prefab does not contain {nameof(IMagnetUpgradeData)}!");
            return car;
        }

        _magnetSetingsProvider.Set(magnetData);

        _signalBus.Fire(new CarSpawnedSignal(car)
        {
           // Car = car
        });

        Debug.Log($"Created car: {car.name}");
        return car;
    }   
}
