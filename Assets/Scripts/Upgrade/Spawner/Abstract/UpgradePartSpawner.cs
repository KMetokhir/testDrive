using UnityEngine;

public abstract class UpgradePartSpawner : MonoBehaviour
{   
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _parent;

    public virtual  UpgradePart LastSpawnedPart { get; private set; }

    public virtual bool TrySpawn(UpgradePart part)
    {
        if (IsSpawnPossible(part))
        {
            part.transform.position = _spawnPosition.TransformPoint(part.SpawnPosition);
            part.transform.rotation = _spawnPosition.transform.rotation;
            part.transform.parent = _parent;

            LastSpawnedPart = part;

            return true;
        }

        return false;
    }    

    public abstract bool IsSpawnPossible(UpgradePart part);
}
