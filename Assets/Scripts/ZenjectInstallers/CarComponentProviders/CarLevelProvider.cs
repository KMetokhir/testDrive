using System;
using Zenject;

public class CarLevelProvider : CarComponentProvider<ICarLevel>, ICarLevel
{
    public event Action Changed;
    public uint Value => Component.Value;

    public CarLevelProvider(SignalBus signalBus) : base(signalBus) { }

    public override void Dispose()
    {
        if (Component != null)
        {
            Component.Changed -= OnComponentValueChanged;
        }

        base.Dispose();
    }

    protected override bool TryGetComponent(CarConteiner car, out ICarLevel component)
    {
        if (base.TryGetComponent(car, out component))
        {
            if (Component != null)
            {
                Component.Changed -= Changed;
            }

            component.Changed += OnComponentValueChanged;

            return true;
        }

        return false;
    }

    private void OnComponentValueChanged()
    {
        Changed?.Invoke();
    }
}