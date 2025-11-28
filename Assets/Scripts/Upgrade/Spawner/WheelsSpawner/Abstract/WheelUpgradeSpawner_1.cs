using System;


public abstract class WheelUpgradeSpawner :  GenericUpgradeSpawner<WheelUpgradePart> // has to be ObservablePartSpawner<WheelUpgradePart>
{
    public abstract event Action<WheelUpgradePart> WheelSpawned;
}
