using UnityEngine;

public class TrunkSettings : Settings<PowerUpgrader, IPowerUpgradeData>, ITrunkData
{
    public uint MaxWeight { get; private set; }

    protected override void ApplyUpgrade(IPowerUpgradeData data)
    {
        MaxWeight = data.MaxWeight;
        Debug.Log($"TrunkSettings: MaxWeight updated to {MaxWeight}");
    }
}
