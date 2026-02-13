
using System;

public class DrivenWheelSpawnerContainer : GenericSpawnersContainer<DrivingWheelUpgrade, DrivenWheelSpawner>, IWheelSpawnersContainer
{
    event Action<WheelUpgradePart> IWheelSpawnersContainer.PartSpawned
    {
        add
        {
            base.PartSpawned += value;
        }

        remove
        {
            base.PartSpawned -= value;
        }
    }
}
