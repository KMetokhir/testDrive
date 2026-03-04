using UnityEngine;
using Zenject;

public class RuntimeCarFactory 
{
    private readonly DiContainer _container;
    private CarConteiner _currentPrefab;
    private readonly SignalBus _signalBus;

    private CarConteiner CurrentCar { get;  set; } // not nessesary tmp

    private MagnetSetingsProvider _magnetSetingsProvider;   
    private DriveDataProvader _driveDataProvader;

    [Inject]
    public RuntimeCarFactory(
        DiContainer container,
        SignalBus signalBus,
        CarConteiner defaultPrefab,
        MagnetSetingsProvider magnetSetingsProvider,
        DriveDataProvader driveDataProvader)
    {
        _container = container;
        _signalBus = signalBus;
        _currentPrefab = defaultPrefab;
        _magnetSetingsProvider = magnetSetingsProvider;
        _driveDataProvader = driveDataProvader;
    }

    public void SetCarPrefab(CarConteiner newPrefab)
    {
        _currentPrefab = newPrefab;
    }

    public CarConteiner CreateCar()
    {
        var car = _container.InstantiatePrefabForComponent<CarConteiner>(_currentPrefab);       

        CurrentCar = car;

        IMagnetData magnetData = car.GetComponent<IMagnetData>();
        IDriveData driverData = car.GetComponent<IDriveData>();

        if (magnetData == null)
        {
            Debug.LogError($"Car prefab does not contain {nameof(IMagnetData)}!");
            return car;
        }

        if (driverData == null)
        {
            Debug.LogError($"Car prefab does not contain {nameof(IDriveData)}!");
            return car;
        }

        _magnetSetingsProvider.Set(magnetData);
        _driveDataProvader.Set(driverData);

        _signalBus.Fire(new CarSpawnedSignal(car)
        {
           // Car = car
        });

        Debug.Log($"Created car: {car.name}");
        return car;
    }   
}
