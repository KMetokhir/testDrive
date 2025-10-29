
public class GenericUpgradeSpawner<T> : UpgradePartsSpawner
    where T : UpgradePart
{
    public override bool IsSpawnPossible(UpgradePart part)
    {
        if (part is T)
        {
            return true;
        }

        return false;
    }
}