using System;

public interface IWheelSpawnersContainer
{
    public event Action<WheelUpgradePart> PartSpawned;
}