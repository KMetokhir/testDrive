
public class CraneSpawner : ObservablePartSpawner<Crane>
{
    public override bool TrySpawn(UpgradePart part)  // same as magnet
    {
        if (base.TrySpawn(part))
        {
            part.transform.parent = null;

            return true;
        }

        return false;
    }
}
