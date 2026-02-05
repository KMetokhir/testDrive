using System;
using UnityEngine;

public class ObservablePartSpawner<T> : GenericUpgradeSpawner<T>, IObservablePartSpawner
     where T : class, IUpgradePart
{
    public  event Action<T> TypedPartSpawned;

    public event Action<IUpgradePart> PartSpawned;

    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {
            TypedPartSpawned?.Invoke(part as T);
            PartSpawned?.Invoke(part);

            return true;
        }

        return false;
    }
}
