using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class GenericObservableSpawner<T> : GenericUpgradeSpawner<T>//, IObservablePartSpawner
     where T : class, IUpgradePart
{
    public virtual event Action<T> PartSpawned;

 //   public event Action<IUpgradePart> PartSpawned;

    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {           
          

            PartSpawned?.Invoke(part as T);
           // PartSpawned?.Invoke(part);

            return true;
        }

        return false;
    }    
}
