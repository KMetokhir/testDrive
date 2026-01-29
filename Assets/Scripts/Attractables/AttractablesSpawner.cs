using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttractablesSpawner : MonoBehaviour
{
    [SerializeField] private List<QuadSpawnArea> _quadSpawnArias;
    [SerializeField] private Attractable _objectPrefab;
    [SerializeField] float rowsPerQuad;
    [SerializeField] float spawnPointsPerRow;
    [SerializeField] AttractablesSaveLoader _saveLoader;

 //   [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    private string _currentScene => SceneManager.GetActiveScene().name;

    private List<Vector3> _spawnPoints;

    public event Action<Attractable> AttractableSpawned;

    private void Start()
    {
        /*foreach (var obj in _quadSpawnArias)
        {
            VisualizeSpawnPoints(GenerateQuadSpawnPoints(obj), obj.transform);
        }*/

        AttractablesType type = _objectPrefab.Type;
    }

    private List<Vector3> GenerateQuadSpawnPoints(QuadSpawnArea quad)
    {
        List<Vector3> spawnPoints = new List<Vector3>();

        // Calculate quad boundaries
        float quadHalfWidth = quad.SizeX / 2f;
        float quadHalfHeight = quad.SizeY / 2f;

        // Calculate row spacing
        float rowSpacing = quad.SizeY / (rowsPerQuad + 1);

        for (int row = 0; row < rowsPerQuad; row++)
        {
            // Calculate row position (z-axis in Unity's 3D space)
            float rowZ = quad.Center.z - quadHalfHeight + (row + 1) * rowSpacing;

            // Calculate points in this row
            float pointSpacing = quad.SizeX / (spawnPointsPerRow + 1);

            for (int point = 0; point < spawnPointsPerRow; point++)
            {
                float pointX = quad.Center.x - quadHalfWidth + (point + 1) * pointSpacing;

                // Create spawn point
                Vector3 spawnPoint = new Vector3(
                    pointX,
                   quad.Center.y + 0.1f, // Slightly above quad
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
