using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelUpgradeSpawner<T> : GenericUpgradeSpawner<T>
    where T :class, IWheelUpgrade
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
