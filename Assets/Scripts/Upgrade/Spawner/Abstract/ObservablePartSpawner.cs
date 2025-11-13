using System;
using UnityEngine;

public class ObservablePartSpawner<T> : GenericUpgradeSpawner<T>
     where T : class, IUpgradePart
{
    public event Action<T> PartSpawned;

    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {
            PartSpawned?.Invoke(part as T);
            return true;
        }

        return false;
    }
}
