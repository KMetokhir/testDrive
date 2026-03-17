using UnityEngine;
using Zenject;

public class CameraFollower : MonoBehaviour // test
{
    private CarConteiner _car;
    private SignalBus _signalBus;

    [Inject]
    void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    void OnEnable()
    {
        _signalBus.Subscribe<CarSpawnedSignal>(OnCarSpawned);
    }

    void OnDisable()
    {
        _signalBus.Unsubscribe<CarSpawnedSignal>(OnCarSpawned);
    }

    private void OnCarSpawned(CarSpawnedSignal signal)
    {
        _car = signal.Car;
    }   
}