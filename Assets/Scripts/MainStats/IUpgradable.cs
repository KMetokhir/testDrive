using System;

public interface IUpgradable
{
    public uint UpgradeLevel { get; }

    public event Action Upgraded;
}
