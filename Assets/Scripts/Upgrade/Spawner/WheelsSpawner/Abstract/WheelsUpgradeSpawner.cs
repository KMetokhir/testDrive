using System;


public abstract class WheelsUpgradeSpawner : GenericUpgradeSpawner<WheelUpgradePart> // has to be ObservablePartSpawner<WheelUpgradePart>
{
    public abstract event Action<WheelUpgradePart> WheelSpawned;
}
