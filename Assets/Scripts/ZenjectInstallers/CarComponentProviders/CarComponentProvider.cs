using System;
using Zenject;

public abstract class CarComponentProvider<T> : IInitializable, IDisposable
{
    private readonly SignalBus _signalBus;

    protected T Component { get; private set; }

    protected CarComponentProvider(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<CarSpawnedSignal>(OnCarSpawned);
    }

    public virtual void Dispose()
    {
        _signalBus.Unsubscribe<CarSpawnedSignal>(OnCarSpawned);
    }

    protected virtual bool TryGetComponent(CarConteiner car, out T component)
    {
        component = car.GetComponentInChildren<T>();

        return component != null;
    }

    private void OnCarSpawned(CarSpawnedSignal signal)
    {
        if (TryGetComponent(signal.Car, out T component))
        {
            Component = component;
        }
        else
        {
            throw new Exception(
               $"Car prefab does not contain {typeof(T).Name}!");
        }
    }
}