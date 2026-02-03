using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField] private List<QuadSpawnArea> _quadSpawnArias;
    [SerializeField] private int _rowsPerQuad;
    [SerializeField] private int _columnsPerQuad;

    [Inject]
    [SerializeField] private AttractablesSpawner _spawner;

    private void Start()
    {
      

        _spawner.Spawn(AttractablesType.screw, _quadSpawnArias, _rowsPerQuad, _columnsPerQuad);
    }
}
