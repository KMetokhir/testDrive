using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Zenject;

public class AttractablesSpawner : MonoBehaviour
{
    /* [Inject]
     [SerializeField] private SpawnerLoader spawnerLoader;*/

    // [SerializeField] private List<QuadSpawnArea> _quadSpawnArias;

    [SerializeField] private Attractable _objectPrefab;
    [SerializeField] private AttractableDataHandler _dataHandler;
    [SerializeField] private AttractableGenerator<Attractable> _generator;

    private int _rowsPerQuad;
    private int _columnsPerQuad;

    private string _currentSceneName => SceneManager.GetActiveScene().name; // use sceneload handler

    int count = 1; //test

    //   private List<Vector3> _spawnPoints;

    public event Action<Attractable> AttractableSpawned;

    /* private void Start()
     {

         AttractablesType type = _objectPrefab.Type;

         List<Vector3> spawnPoints = _dataHandler.GetPositionsOnScene(_currentSceneName, type);
         _dataHandler.RemoveAllDataOnScene(_currentSceneName);


         if (spawnPoints.Count != 0)
         {
             foreach (Vector3 position in spawnPoints)
             {
                 SpawnObject(_objectPrefab, position);
             }
         }
         else
         {
             foreach (var obj in _quadSpawnArias)
             {
                 spawnPoints = GenerateQuadSpawnPoints(obj);

                 foreach (Vector3 position in spawnPoints)
                 {
                     SpawnObject(_objectPrefab, position);
                 }
             }
         }

         // _dataHandler.GetPositionsOnScene(_currentSceneName, type);
     }*/


    public void Spawn(AttractablesType type, List<QuadSpawnArea> _quadSpawnArias, int rows, int columns)
    {
        _rowsPerQuad = rows;
        _columnsPerQuad = columns;

        List<Vector3> spawnPoints = _dataHandler.GetPositionsOnScene(_currentSceneName, type, _rowsPerQuad, _columnsPerQuad);
        _dataHandler.RemoveAllDataOnScene(_currentSceneName);

        // spawnPoints.Clear(); // TMP

        if (spawnPoints.Count != 0)
        {
            foreach (Vector3 position in spawnPoints)
            {
                SpawnObject(position);
            }
        }
        else
        {
            foreach (var obj in _quadSpawnArias)
            {
                spawnPoints = GenerateQuadSpawnPoints(obj);

                foreach (Vector3 position in spawnPoints)
                {
                    SpawnObject(position);
                }
            }
        }
    }

    private void SpawnObject(Vector3 spawnPoint)
    {
        Attractable attractable = _generator.Generate();
        attractable.transform.position = spawnPoint;

        _dataHandler.RegisterObject(attractable, _currentSceneName, _rowsPerQuad, _columnsPerQuad);

        /* if (count == 1)
         {
             attractable.Collect();
             Debug.Log("Collected " + attractable.Id);
             count--;
         }*/

        // attractable.Deactivated += OnDeactivated;
    }

    /* private void OnDeactivated(Attractable attractable)
     {
         attractable.Deactivated -= OnDeactivated;
         Debug.Log(attractable.Id + " REMOVED INSPAWNER");
         _dataHandler.RemoveById(attractable.Id);

     }*/

    private List<Vector3> GenerateQuadSpawnPoints(QuadSpawnArea quad)
    {
        if (_columnsPerQuad == 0 || _rowsPerQuad == 0)
        {
            throw new Exception($"Columns {_columnsPerQuad} or Rows {_rowsPerQuad} can not be equal 0");
        }

        List<Vector3> spawnPoints = new List<Vector3>();

        float yOffset = 0.1f;

        float quadHalfWidth = quad.SizeX / 2f;
        float quadHalfHeight = quad.SizeY / 2f;

        float rowSpacing = quad.SizeY / (_rowsPerQuad + 1);

        for (int row = 0; row < _rowsPerQuad; row++)
        {
            float rowZ = quad.Center.z - quadHalfHeight + (row + 1) * rowSpacing;

            float pointSpacing = quad.SizeX / (_columnsPerQuad + 1);

            for (int point = 0; point < _columnsPerQuad; point++)
            {
                float pointX = quad.Center.x - quadHalfWidth + (point + 1) * pointSpacing;

                Vector3 spawnPoint = new Vector3(
                    pointX,
                   quad.Center.y + yOffset,
                    rowZ
                );

                spawnPoints.Add(spawnPoint);
            }
        }

        return spawnPoints;
    }

    private void VisualizeSpawnPoints(List<Vector3> spawnPoints, Transform parent)
    {
        foreach (Vector3 point in spawnPoints)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = point;
            sphere.transform.localScale = Vector3.one * 0.1f;
            sphere.transform.SetParent(parent);
            sphere.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
