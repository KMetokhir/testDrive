using UnityEngine;
public class CraneSpawner : GenericObservableSpawner<Crane>
{
   /* [SerializeField] private Transform _parentContainer;

    public override bool TrySpawn(UpgradePart part)  // same as magnet
    {
        if (base.TrySpawn(part))
        {
            part.transform.parent = _parentContainer;

            return true;
        }

        return false;
    }*/
}
