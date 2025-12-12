using UnityEngine;
using System.IO;

public class SOUser : MonoBehaviour
{
    [SerializeField]  public TestSO myData;
    private string savePath;

   

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "scriptableObjectSave.txt");
       // LoadData();
    }

    void OnApplicationQuit()
    {
       // SaveData();
    }

    public void SaveData()
    {
        if (myData != null)
        {
            string dataName = myData.name;
            File.WriteAllText(savePath, dataName);
        }
    }

    public void LoadData()
    {
        if (File.Exists(savePath))
        {
            string dataName = File.ReadAllText(savePath);
            // Load ScriptableObject by name from Resources or AssetDatabase
            myData = Resources.Load<TestSO>(dataName);
            if (myData == null)
            {
                Debug.LogWarning("ScriptableObject not found: " + dataName);
            }
        }
    }
}
