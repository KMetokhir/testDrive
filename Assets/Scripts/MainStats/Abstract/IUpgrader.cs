using System;

public interface IUpgrader<T> where T : IUpgradeData
{
    event Action<T> UpgradeExecuted;
}
