using System;

public abstract class ObservableUpgradePart : UpgradePart
{
    public event Action<ObservableUpgradePart> Destroied;

    protected override void MakeInDestroy()
    {
        Destroied?.Invoke(this);
    }
}
