using System;

public interface ITrunkData :IPowerUpgradeData
{
    public event Action Changed;
}
