using UnityEngine;

public abstract class UpgradePartsSpawner : MonoBehaviour
{
    [SerializeField] private Transform _parent;    

    public virtual bool TrySpawn(UpgradePart part)
    {
        if (IsSpawnPossible(part))
        {
            part.transform.position = _parent.TransformPoint(part.SpawnPosition);
            part.transform.rotation = _parent.transform.rotation;
            part.transform.parent = _parent;

            return true;
        }

        return false;
    }

    public abstract bool IsSpawnPossible(UpgradePart part);
}
