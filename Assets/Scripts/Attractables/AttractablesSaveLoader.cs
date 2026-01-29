using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AttractablesSaveLoader : MonoBehaviour
{
    private const string AllObjectsIdKey = "AllObjects";
    private const string ObjectKeyPrefix = "Object_";

    //  private Dictionary<string, List<AttractableData>> _allScenesAttractables = new Dictionary<string, List<AttractableData>>(); // IattractableData to methods

    private List<AttractableData> _alldata = new List<AttractableData>();
    private List<string> _allId => _alldata.Select(val => val.Id).ToList();    

    public void RegisterObject(Attractable attractable, string sceneName)
    {
        if (attractable == null)
            throw new System.Exception($"{nameof(attractable)}  is null");

        bool isObjectregistered = _alldata.First(val => val.Id == attractable.Id) != null;

        if (isObjectregistered)
        {
            throw new System.Exception($"Attractable {nameof(attractable)} registered allresdy");
        }
        else
        {
            AttractableData data = new AttractableData(attractable.Id, attractable.transform.position, attractable.Type, sceneName);
            string key = $"{ObjectKeyPrefix}{attractable.Id}";
            PlayerPrefs.SetString(key, data.ToString());

            UpdateObjectList();

            _alldata.Add(data);

            attractable.Deactivated += Remove;

            Debug.Log($"Registered object: {attractable.Id} at {attractable.Transform.position}");
        }
    }

    public void Remove(Attractable attractable)
    {

        AttractableData data = _alldata.First(val => val.Id == attractable.Id);

        if (data == null)
        {
            throw new Exception($"attractable does not in list");
        }

        attractable.Deactivated -= Remove;

        _alldata.Remove(data);
        PlayerPrefs.DeleteKey($"{ObjectKeyPrefix}{attractable.Id}");
        UpdateObjectList();
        Debug.Log($"Removed object: {attractable.Id}");
    }

    private void UpdateObjectList()
    {
        string allIdData = string.Join(",", _allId);
        PlayerPrefs.SetString(AllObjectsIdKey, allIdData);
        PlayerPrefs.Save();
    }

    public List<Vector3> GetObjectPositions(string sceneName, AttractablesType type)
    {
        List<Vector3> positions = new List<Vector3>();

        List<AttractableData> sceneTypeData = _alldata.Where(val => val.SceneName == sceneName && val.Type == type).ToList();

        foreach (AttractableData data in sceneTypeData)
        {
            positions.Add(data.Position);
        }

        Debug.Log($"Found {positions.Count} objects of type '{type}' in scene '{sceneName}'");

        return positions;
    }

    private void LoadAllObjects()
    {
        _alldata.Clear();

        if (PlayerPrefs.HasKey(AllObjectsIdKey))
        {
            string objectList = PlayerPrefs.GetString(AllObjectsIdKey);
            string[] objectIds = objectList.Split(',');

            foreach (string id in objectIds)
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

    [System.Serializable]
    public class AttractableData
    {
        public readonly string Id;
        public readonly Vector3 Position;
        public readonly AttractablesType Type;
        public readonly string SceneName;

        public AttractableData(string id, Vector3 pos, AttractablesType type, string scene)
        {
            Id = id;
            Position = pos;
            Type = type;
            SceneName = scene;
        }

        public AttractableData(string savedString)
        {
            try
            {
                string[] parts = savedString.Split('|');
                if (parts.Length >= 4)
                {
                    Id = parts[0];

                    string[] posParts = parts[1].Split(',');
                    Position = new Vector3(
                        float.Parse(posParts[0]),
                        float.Parse(posParts[1]),
                        float.Parse(posParts[2])
                    );

                    if (Enum.TryParse(parts[2], out AttractablesType type))
                    {
                        Type = type;
                    }
                    else
                    {
                        throw new Exception($"type {parts[2]} doesn't excist");
                    }

                    SceneName = parts[3];
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error parsing SceneObjectData: {e.Message}");
            }
        }

        public override string ToString()
        {
            return $"{Id}|{Position.x},{Position.y},{Position.z}|{Type}|{SceneName}";
        }
    }


    /* public void UpdateObjectPosition(string objectId, Vector3 newPosition)
     {
         if (allData.TryGetValue(objectId, out AttractableData data))
         {
             data.position = newPosition;
             SaveObjectData(data);
             Debug.Log($"Updated position for {objectId}: {newPosition}");
         }
         else
         {
             Debug.LogWarning($"Object ID not found: {objectId}");
         }
     }*/

    /*
     public List<SceneObjectData> GetObjects(string sceneName, string objectType)
     {
         return allObjects.Values
             .Where(obj => obj.sceneName == sceneName && obj.objectType == objectType)
             .ToList();
     }


     public List<SceneObjectData> GetObjectsInScene(string sceneName)
     {
         return allObjects.Values
             .Where(obj => obj.sceneName == sceneName)
             .ToList();
     }

   
     public List<string> GetAllScenes()
     {
         return allObjects.Values
             .Select(obj => obj.sceneName)
             .Distinct()
             .ToList();
     }

   
     public List<string> GetObjectTypesInScene(string sceneName)
     {
         return allObjects.Values
             .Where(obj => obj.sceneName == sceneName)
             .Select(obj => obj.objectType)
             .Distinct()
             .ToList();
     }





    /*public AttractableData(string savedString)
    {
        try
        {
            string[] parts = savedString.Split('|');
            if (parts.Length >= 4)
            {
                objectId = parts[0];

                string[] posParts = parts[1].Split(',');
                position = new Vector3(
                    float.Parse(posParts[0]),
                    float.Parse(posParts[1]),
                    float.Parse(posParts[2])
                );

                if (Enum.TryParse(parts[2], out AttractablesType type))
                {
                    objectType = type;
                }
                else
                {
                    throw new Exception($"type {parts[2]} doesn't excist");
                }

                sceneName = parts[3];
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error parsing SceneObjectData: {e.Message}");
        }
    }*/



    /* public void RemoveObjectsByType(string sceneName, string objectType)
     {
         List<string> idsToRemove = new List<string>();

         foreach (var kvp in allObjects)
         {
             if (kvp.Value.sceneName == sceneName && kvp.Value.objectType == objectType)
             {
                 idsToRemove.Add(kvp.Key);
             }
         }

         foreach (string id in idsToRemove)
         {
             allObjects.Remove(id);
             PlayerPrefs.DeleteKey($"{OBJECT_DATA_PREFIX}{id}");
         }

         if (idsToRemove.Count > 0)
         {
             UpdateObjectList();
             Debug.Log($"Removed {idsToRemove.Count} objects of type '{objectType}' from scene '{sceneName}'");
         }
     }

    
     public void ClearScene(string sceneName)
     {
         List<string> idsToRemove = new List<string>();

         foreach (var kvp in allObjects)
         {
             if (kvp.Value.sceneName == sceneName)
             {
                 idsToRemove.Add(kvp.Key);
             }
         }

         foreach (string id in idsToRemove)
         {
             allObjects.Remove(id);
             PlayerPrefs.DeleteKey($"{OBJECT_DATA_PREFIX}{id}");
         }

         if (idsToRemove.Count > 0)
         {
             UpdateObjectList();
             Debug.Log($"Cleared {idsToRemove.Count} objects from scene '{sceneName}'");
         }
     }

   
     public void ClearAllData()
     {
         allObjects.Clear();

         
         if (PlayerPrefs.HasKey(OBJECT_LIST_KEY))
         {
             string[] objectIds = PlayerPrefs.GetString(OBJECT_LIST_KEY).Split(',');

             foreach (string id in objectIds)
             {
                 if (!string.IsNullOrEmpty(id))
                 {
                     PlayerPrefs.DeleteKey($"{OBJECT_DATA_PREFIX}{id}");
                 }
             }
         }

         PlayerPrefs.DeleteKey(OBJECT_LIST_KEY);
         PlayerPrefs.Save();

         Debug.Log("Cleared all object data");
     }

     
     public void SaveAll()
     {
         foreach (var kvp in allObjects)
         {
             SaveObjectData(kvp.Value);
         }
         PlayerPrefs.Save();
         Debug.Log($"Saved {allObjects.Count} objects to PlayerPrefs");
     }

   

     }*/







































    /* public void SaveAttractable(Attractable attractable, string sceneName)
     {
         if (_activeAttractables.TryGetValue(attractable.Type, out List<Attractable> attractables))
         {
             attractables.Add(attractable);
         }
         else
         {
             List<Attractable> newList = new List<Attractable>();
             newList.Add(attractable);
             _activeAttractables.Add(attractable.Type, newList);
         }

         string id  = attractable.Type.ToString()+ Divider+counter.ToString();
         counter++;
     }

     private string GenerateUniqueId(AttractablesType objectType)
     {
         string timestamp = DateTime.Now.Ticks.ToString();
         string random = UnityEngine.Random.Range(1000, 9999).ToString();
         return $"{objectType}_{timestamp}_{random}";
     }

     private void DeleteAtractable(Attractable attractable)
     {
         _activeAttractables.Remove(attractable);
     }

     private string GetData(Attractable attractable)
     {
         return attractable.Transform.position.ToString() ;
     }

     private string Vector3ToString(Vector3 vector)
     {
         return vector.ToString();
     }

     public static Vector3 StringToVector3(string vectorString)
     {
         try
         {
             // Clean up the string
             string cleanString = vectorString
                 .Replace("(", "")     // Remove parentheses if present
                 .Replace(")", "")
                 .Replace(" ", "");    // Remove spaces

             // Split by comma
             string[] parts = cleanString.Split(',');

             // Check we have exactly 3 parts
             if (parts.Length != 3)
             {
                 Debug.LogError($"Invalid Vector3 string format. Expected 3 parts, got {parts.Length}: {vectorString}");
                 return Vector3.zero;
             }

             // Parse each part to float
             float x = float.Parse(parts[0]);
             float y = float.Parse(parts[1]);
             float z = float.Parse(parts[2]);

             return new Vector3(x, y, z);
         }
         catch (Exception e)
         {
             Debug.LogError($"Error parsing Vector3 from string '{vectorString}': {e.Message}");
             return Vector3.zero;
         }
     }

     public static void SaveStringList(string key, List<string> list)
     {
         string serialized = string.Join("|", list.ToArray());
         PlayerPrefs.SetString(key, serialized);
     }

     // Load List of strings
     public static List<string> LoadStringList(string key)
     {
         List<string> list = new List<string>();

         if (PlayerPrefs.HasKey(key))
         {
             string serialized = PlayerPrefs.GetString(key);
             string[] items = serialized.Split('|');
             list.AddRange(items);
         }

         return list;
     }

     // Save Vector3
     public static void SaveVector3(string key, Vector3 vector)
     {
         PlayerPrefs.SetFloat($"{key}_x", vector.x);
         PlayerPrefs.SetFloat($"{key}_y", vector.y);
         PlayerPrefs.SetFloat($"{key}_z", vector.z);
     }

     // Load Vector3
     public static Vector3 LoadVector3(string key, Vector3 defaultValue = default)
     {
         if (PlayerPrefs.HasKey($"{key}_x"))
         {
             float x = PlayerPrefs.GetFloat($"{key}_x");
             float y = PlayerPrefs.GetFloat($"{key}_y");
             float z = PlayerPrefs.GetFloat($"{key}_z");
             return new Vector3(x, y, z);
         }
         return defaultValue;
     }
 }
 */






}
