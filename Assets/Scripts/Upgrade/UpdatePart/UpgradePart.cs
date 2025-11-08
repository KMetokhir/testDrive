using UnityEngine;

public abstract class UpgradePart : MonoBehaviour, IUpgradePart

{
    [SerializeField] private Vector3 _spawnLocalPosition;
    [SerializeField] private uint _count;

    public Vector3 SpawnPosition => _spawnLocalPosition;
    public uint Count => _count;
}
