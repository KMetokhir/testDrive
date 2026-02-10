using System;

public interface IObservablePartSpawner
{
    event Action<IUpgradePart> PartSpawned;
}