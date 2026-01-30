using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AttractablesSpawner : MonoBehaviour
{
    [SerializeField] private List<QuadSpawnArea> _quadSpawnArias;
    [SerializeField] private Attractable _objectPrefab;
    [SerializeField] float rowsPerQuad;
    [SerializeField] float spawnPointsPerRow;
    [SerializeField] AttractableDataHandler _dataHandler;

    private string _currentSceneName => SceneManager.GetActiveScene().name; // use sceneload handler

    private List<Vector3> _spawnPoints;

    public event Action<Attractable> AttractableSpawned;

    private void Start()
    { 

        AttractablesType type = _objectPrefab.Type;

        List<Vector3> spawnPoints = _dataHandler.GetPositionsOnScene(_currentSceneName, type);
        _dataHandler.RemoveAllDataOnScene(_currentSceneName);
        int count = 0;

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

        _dataHandler.GetPositionsOnScene(_currentSceneName, type);
    }

    public void SpawnObject(Attractable objPrefab, Vector3 spawnPoint)
    {
        Attractable attractable = Instantiate(objPrefab);
        attractable.transform.position = spawnPoint;
        _dataHandler.RegisterObject(attractable, _currentSceneName);

        attractable.Deactivated += onDeactivated;
    }

    private void onDeactivated(Attractable attractable)
    {
        attractable.Deactivated -= onDeactivated;
        Debug.Log(attractable.Id + " REMOVED INSPAWNER");
        _dataHandler.RemoveById(attractable.Id);

    }

    private List<Vector3> GenerateQuadSpawnPoints(QuadSpawnArea quad)
    {
        List<Vector3> spawnPoints = new List<Vector3>();

        float yOffset = 0.1f;


        float quadHalfWidth = quad.SizeX / 2f;
        float quadHalfHeight = quad.SizeY / 2f;

      
        float rowSpacing = quad.SizeY / (rowsPerQuad + 1);

        for (int row = 0; row < rowsPerQuad; row++)
        {      
            float rowZ = quad.Center.z - quadHalfHeight + (row + 1) * rowSpacing;
                        
            float pointSpacing = quad.SizeX / (spawnPointsPerRow + 1);

            for (int point = 0; point < spawnPointsPerRow; point++)
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
