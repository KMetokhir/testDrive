using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class AttractableDataHandler : MonoBehaviour
{
    private const string AllObjectsIdKey = "AllObjects";
    private const string ObjectKeyPrefix = "Object_";

    //  private Dictionary<string, List<AttractableData>> _allScenesAttractables = new Dictionary<string, List<AttractableData>>(); // IattractableData to methods

    private List<AttractableData> _alldata = new List<AttractableData>();
    private List<string> _allId => _alldata.Select(val => val.Id).ToList();

    private void Awake()
    {
        //_allId.Clear();
        LoadAllObjects();
    }

    public void RegisterObject(Attractable attractable, string sceneName, int rows, int columns)
    {
        if (attractable == null)
            throw new System.Exception($"{nameof(attractable)}  is null");

        string dataId = GenerateDataId(attractable.Id, sceneName);

        bool isObjectregistered = _alldata.FirstOrDefault(val => val.Id == dataId) != null;

        if (isObjectregistered)
        {
            throw new System.Exception($"Attractable {nameof(attractable)} registered allresdy");
        }
        else
        {

            AttractableData data = new AttractableData(dataId, attractable.transform.position, attractable.Type, sceneName, rows, columns);
            string key = $"{ObjectKeyPrefix}{dataId}";
            PlayerPrefs.SetString(key, data.ToString());
            PlayerPrefs.Save();

            _alldata.Add(data);

            UpdateObjectList();

            Debug.Log($"Registered object: {data.Id} at {attractable.Transform.position}");
        }
    }

    public void UpdateObjectList()
    {
        string allIdData = string.Join(",", _allId);
        PlayerPrefs.SetString(AllObjectsIdKey, allIdData);
        PlayerPrefs.Save();
    }

    public void RemoveAllDataOnScene(string sceneName)
    {
        List<AttractableData> sceneData = _alldata.Where(val => val.SceneName == sceneName).ToList();

        foreach (var data in sceneData)
        {
            RemoveData(data.Id);
        }
    }
    public void RemoveById(string id, string sceneName)
    {
        string dataId = GenerateDataId(id, sceneName);

        RemoveData(dataId);
    }

    private void RemoveData(string dataId)
    {
        AttractableData data = _alldata.FirstOrDefault(val => val.Id == dataId);

        if (data == null)
        {
            throw new Exception($"attractable does not in list");
        }

        _alldata.Remove(data);
        PlayerPrefs.DeleteKey($"{ObjectKeyPrefix}{data.Id}");
        PlayerPrefs.Save();
        UpdateObjectList();
        Debug.Log($"Removed object: {data.Id}");

    }

    public List<Vector3> GetPositionsOnScene(string sceneName, AttractablesType type, int rows, int columns)
    {
        List<Vector3> positions = new List<Vector3>();

        List<AttractableData> foundData = _alldata.Where(val => val.SceneName == sceneName && val.Type == type && val.Rows == rows && val.Columns == columns).ToList();

        foreach (AttractableData data in foundData)
        {
            Debug.Log(data.Rows + " columns " + data.Columns);
            positions.Add(data.Position);
        }

        Debug.Log($"Found {positions.Count} objects of type '{type}' in scene '{sceneName}'");

        return positions;
    }

    public void LoadAllObjects()
    {
        _alldata.Clear();

        if (PlayerPrefs.HasKey(AllObjectsIdKey))
        {
            string objectList = PlayerPrefs.GetString(AllObjectsIdKey);
            string[] dataIds = objectList.Split(',');

            foreach (string id in dataIds)
            {
                if (!string.IsNullOrEmpty(id))
                {
                    string key = $"{ObjectKeyPrefix}{id}";

                    if (PlayerPrefs.HasKey(key))
                    {
                        string savedData = PlayerPrefs.GetString(key);
                        AttractableData data = new AttractableData(savedData);
                        _alldata.Add(data);
                    }
                }
            }

            Debug.Log($"Loaded {_alldata.Count} objects from PlayerPrefs");
        }
    }

    private string GenerateDataId(string id, string sceneName)
    {
        char divider = '_';

        return sceneName + divider + id;
    }

    /* private string GetObjectId(string dataId)
     {
         char divider = '_';
         int objectIdIndex = 1;

         string[] parts = dataId.Split(divider);

         if (parts.Length != 2)
         {
             throw new Exception($"incorrect data Id  {dataId}");
         }

         return parts[objectIdIndex];
     }*/
}

[System.Serializable]
public class AttractableData
{
    public readonly string Id;
    public readonly Vector3 Position;
    public readonly AttractablesType Type;
    public readonly string SceneName;
    public readonly int Rows;
    public readonly int Columns;

    public AttractableData(string id, Vector3 pos, AttractablesType type, string scene, int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
        {
            throw new ArgumentNullException("Rows and Columns can not be equal 0");
        }

        Id = id;
        Position = pos;
        Type = type;
        SceneName = scene;
        Rows = rows;
        Columns = columns;
    }

    public AttractableData(string savedString)
    {
        try
        {
            // Debug.Log(savedString);

            string[] parts = savedString.Split('|');

            //Debug.Log(parts[0]);

            if (parts.Length >= 4)
            {
                Id = parts[0];

                string[] position = parts[1].Split('&');
                Position = new Vector3(
                    float.Parse(position[0]),
                    float.Parse(position[1]),
                    float.Parse(position[2])
                );
                // Debug.Log($"{position[0]} {position[1]} {position[2]}");

                if (Enum.TryParse(parts[2], out AttractablesType type))
                {
                    Type = type;
                }
                else
                {
                    throw new Exception($"type {parts[2]} doesn't excist");
                }

                SceneName = parts[3];

                if (int.TryParse(parts[4], out int rows))
                {
                    Rows = rows;
                }
                else
                {
                    Console.WriteLine("Invalid number format! " + parts[4]);
                }

                if (int.TryParse(parts[5], out int columns))
                {
                    Columns = columns;
                }
                else
                {
                    Console.WriteLine("Invalid number format! " + parts[5]);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing SceneObjectData: {e.Message}");
        }
    }

    public override string ToString()
    {
        return $"{Id}|{Position.x}&{Position.y}&{Position.z}|{Type}|{SceneName}|{Rows}|{Columns}";
    }
}

