using System;
using Zenject;

public class TrunkSettingsProvider : CarComponentProvider<ITrunkData>, ITrunkData
{
    public event Action Changed;
    public uint MaxWeight => Component?.MaxWeight ?? 0;

    public TrunkSettingsProvider(SignalBus signalBus) : base(signalBus) { }

    public override void Dispose()
    {
        if (Component != null)
        {
            Component.Changed -= OnComponentValueChanged;
        }

        base.Dispose();
    }

    protected override bool TryGetComponent(CarConteiner car, out ITrunkData component)
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
