
using System;

public class LeftRotaryWheelSpawnersContainer : GenericSpawnersContainer<LeftRotaryWheelUpgrade, LeftRotaryWheelSpawner>, IWheelSpawnersContainer
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
