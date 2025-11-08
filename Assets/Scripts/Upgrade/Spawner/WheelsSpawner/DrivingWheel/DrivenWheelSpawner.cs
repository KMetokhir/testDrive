using System;

public class DrivenWheelSpawner : WheelUpgradeSpawner<DrivingWheelUpgrade>
{
   /* public event Action<DrivingWheelUpgrade> NewWheelSpawned;

    public override bool TrySpawn(UpgradePart part)
    {
        if (base.TrySpawn(part))
        {
            NewWheelSpawned?.Invoke(part as DrivingWheelUpgrade);

            return true;
        }

        return false;
    }*/
}