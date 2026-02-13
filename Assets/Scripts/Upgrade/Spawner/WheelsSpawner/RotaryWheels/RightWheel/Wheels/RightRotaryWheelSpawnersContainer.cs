
using System;

public class RightRotaryWheelSpawnersContainer : GenericSpawnersContainer<RightRotaryWheelUpgrade,RightRotaryWheelSpawner>, IWheelSpawnersContainer
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
