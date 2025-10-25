using UnityEngine;

public abstract class UpgradePart : MonoBehaviour

{
    [SerializeField] private Vector3 _spawnLocalPosition;    

    public Vector3 SpawnPosition => _spawnLocalPosition;

}
