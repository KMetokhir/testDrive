using UnityEngine;

public interface IUpgradePart
{
    public Vector3 SpawnPosition { get; }
    public uint Count { get; }
}