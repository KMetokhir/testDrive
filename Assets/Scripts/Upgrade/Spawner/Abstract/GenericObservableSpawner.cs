using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class GenericObservableSpawner<T> : GenericUpgradeSpawner<T>
     where T : class, IUpgradePart
{
    public virtual event Action<T> PartSpawned;
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
