using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpawner : GenericObservableSpawner<Magnet>
{
   /* [SerializeField] private Transform _parent;    
    
    public void SetParent(Transform parent)
    {
        _parent = parent;
    }

    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {
            if (_parent != null)
            {
                part.transform.parent = _parent;
            }

            return true;
        }

        return false;
    }*/
}
