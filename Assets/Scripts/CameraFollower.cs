using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollower : MonoBehaviour
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

    void LateUpdate()
    {
        if (_car != null) {
            _car.DoSmth();
        }      
    }
}
