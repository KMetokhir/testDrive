
using UnityEngine;

public class GenericUpgradeSpawner<T> : UpgradePartSpawner
    where T : class, IUpgradePart
{
    public override bool IsSpawnPossible(UpgradePart part)
    {
        return part as T != null;
    }
}