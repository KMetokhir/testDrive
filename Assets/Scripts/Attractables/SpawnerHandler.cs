using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerHandler<T> : MonoBehaviour
    where T : Attractable
{
    [SerializeField] private List<QuadSpawnArea> _quadSpawnArias;
    [SerializeField] private int _rowsPerQuad;
    [SerializeField] private int _columnsPerQuad;

    [Inject]
    [SerializeField] private AttractablesSpawner<T> _spawner;

    private void Awake()
    {
        _spawner.Spawn(AttractablesType.screw, _quadSpawnArias, _rowsPerQuad, _columnsPerQuad);
    }
}
