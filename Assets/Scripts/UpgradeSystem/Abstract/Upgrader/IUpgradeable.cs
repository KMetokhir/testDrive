using System;

public interface IUpgradeable
{
    public event Action Upgraded;
    public uint UpgradeLevel { get; }
}
