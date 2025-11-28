using System.Collections.Generic;

public abstract class CompositePart : ObservableUpgradePart
{
    public abstract List<UpgradePartSpawner> GetSpawners();

    protected override void MakeInDestroy()
    {
        DestroyDependentParts();
        base.MakeInDestroy();
    }

    protected abstract void DestroyDependentParts();
}
