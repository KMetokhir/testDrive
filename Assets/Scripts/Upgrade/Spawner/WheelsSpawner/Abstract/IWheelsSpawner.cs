using System;

public interface IWheelsSpawner   
{
    public event Action<IWheelUpgrade> WheelSpawned;

    public bool TrySpawn(UpgradePart part);
}
