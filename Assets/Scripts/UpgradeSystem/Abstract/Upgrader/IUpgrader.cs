using System;

public interface IUpgrader<T>
    where T : IUpgradeData
{
    public event Action<T> UpgradeExecuted;
}

