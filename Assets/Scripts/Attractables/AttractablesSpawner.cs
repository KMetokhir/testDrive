using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractablesSpawner<T> : MonoBehaviour
    where T : Attractable
{
    [SerializeField] private AttractableDataHandler<T> _dataHandler;
    [SerializeField] private AttractableGenerator<T> _generator;
    [SerializeField] private float _yOffSet;

    private int _rowsPerQuad;
    private int _columnsPerQuad;

    private string _currentSceneName => SceneManager.GetActiveScene().name; // use sceneload handler

    public void Spawn(List<QuadSpawnArea> _quadSpawnArias, int rows, int columns)
    {
        _rowsPerQuad = rows;
        _columnsPerQuad = columns;

        List<Vector3> spawnPoints = _dataHandler.GetPositionsOnScene(_currentSceneName, _rowsPerQuad, _columnsPerQuad);
        _dataHandler.RemoveAllDataOnScene(_currentSceneName);

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
        T attractable = _generator.Generate();
        attractable.transform.position = spawnPoint;

        _dataHandler.RegisterObject(attractable, _currentSceneName, _rowsPerQuad, _columnsPerQuad);
    }

    private List<Vector3> GenerateQuadSpawnPoints(QuadSpawnArea quad)
    {
        if (_columnsPerQuad == 0 || _rowsPerQuad == 0)
        {
            throw new Exception($"Columns {_columnsPerQuad} or Rows {_rowsPerQuad} can not be equal 0");
        }

        List<Vector3> spawnPoints = new List<Vector3>();

        float halfDivider = 2f;
        int additionalOne = 1;

        float quadHalfWidth = quad.SizeX / halfDivider;
        float quadHalfHeight = quad.SizeY / halfDivider;

        float rowSpacing = quad.SizeY / (_rowsPerQuad + additionalOne);

        for (int row = 0; row < _rowsPerQuad; row++)
        {
            float rowZ = quad.Center.z - quadHalfHeight + (row + additionalOne) * rowSpacing;

            float pointSpacing = quad.SizeX / (_columnsPerQuad + additionalOne);

            for (int point = 0; point < _columnsPerQuad; point++)
            {
                float pointX = quad.Center.x - quadHalfWidth + (point + additionalOne) * pointSpacing;

                Vector3 spawnPoint = new Vector3(
                    pointX,
                   quad.Center.y + _yOffSet,
                    rowZ
                );

                spawnPoints.Add(spawnPoint);
            }
        }

        return spawnPoints;
    }

    private void VisualizeSpawnPoints(List<Vector3> spawnPoints, Transform parent) //for tests
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