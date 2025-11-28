using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpawner : ObservablePartSpawner<Magnet>
{
    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {
            part.transform.parent = null;

            return true;
        }

        return false;
    }
}
