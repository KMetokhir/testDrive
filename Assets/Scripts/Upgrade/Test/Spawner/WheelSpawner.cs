using System;

public class WheelSpawner : GenericUpgradeSpawner<WheelUpgrade>
{
    public event Action<WheelUpgrade> NewWheelSpawned;

    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {
            NewWheelSpawned?.Invoke(part as WheelUpgrade);

            return true;
        }

        return false;
    }
}

