
using Unity.VisualScripting;
using UnityEngine;

public class CarDataSaver: ICarPositionSaver, ICarConfigSaver
{
    private const string ConfigKeyPrefix = "CurrentConfig";
    private const string PositionKeyPrefix = "CurrentPosition";
    private const string RotationPrefixKey = "CurrentRotation";
    private const char DataDivider = '_';


    /*private const string WebSessionKey = "WebGL_Session_ID";*/

    private const string XKey = "X";
    private const string YKey = "Y";
    private const string ZKey = "Z";

    public void SaveCarConfig(string upgraderType, uint carLevel, uint upgradeLevel)
    {
        string key = GenerateKey(ConfigKeyPrefix, DataDivider, carLevel.ToString(), upgraderType);

        // SaveInt(key, (int)upgradeLevel);
        PlayerPrefs.SetInt(key, (int)upgradeLevel);
        PlayerPrefs.Save();
    }

    public uint GetCarConfig(string upgraderType, uint carLevel, int defaultValue = 0)
    {
        //  string key = GenereteConfigKey(carLevel, upgraderType);

        string key = GenerateKey(ConfigKeyPrefix, DataDivider, carLevel.ToString(), upgraderType);

        return /*(uint)LoadInt(key, defaultValue);*/(uint)PlayerPrefs.GetInt(key, defaultValue);
    }

    /*  private static string GenereteConfigKey(uint carLevel,string upgraderType)
      {
          return ConfigKeyPrefix + DataDivider+ carLevel + DataDivider+ upgraderType;
      }*/

    // Basic save
    private void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Basic load with default value
    private int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public void SavePosition(uint carLevel, string sceneName, Vector3 position)
    {
        string key = GenerateKey(PositionKeyPrefix, DataDivider, carLevel.ToString(), sceneName);

        SaveVector3(key, position);
    }

    public Vector3 GetPosition(uint carLevel, string sceneName, Vector3 defaultValue = default)
    {
        string key = GenerateKey(PositionKeyPrefix, DataDivider, carLevel.ToString(), sceneName);

        return GetVector3(key, defaultValue);
    }

    private string GenerateKey(string keyPrefix, char dataDivider, params string[] items)
    {
        string key = keyPrefix + dataDivider;

        foreach (string item in items)
        {

            key += (item + dataDivider);

        }

        Debug.LogError(key);

        return key;
    }

    public void SaveRotation(uint carLevel, string sceneName, Quaternion quaternion)
    {
        string key = GenerateKey(RotationPrefixKey, DataDivider, carLevel.ToString(), sceneName);

        Vector3 euler = quaternion.eulerAngles;

        SaveVector3(key, euler);
    }


    public Quaternion GetRotation(uint carLevel, string sceneName, Quaternion defaultValue = default)
    {
        string key = GenerateKey(RotationPrefixKey, DataDivider, carLevel.ToString(), sceneName);

        Vector3 rotation = GetVector3(key, defaultValue.eulerAngles);

        /*string xKey = key + XKey;// repeat
        string yKey = key + YKey;
        string zKey = key + ZKey;

        if (PlayerPrefs.HasKey(xKey))
        {
            float x = PlayerPrefs.GetFloat(xKey);
            float y = PlayerPrefs.GetFloat(key + "_eulerY");
            float z = PlayerPrefs.GetFloat(key + "_eulerZ");

            return Quaternion.Euler(x, y, z);
        }*/

        return Quaternion.Euler(rotation);
    }

    private void SaveVector3(string key, Vector3 position)
    {
        string xKey = key + XKey;
        string yKey = key + YKey;
        string zKey = key + ZKey;

        PlayerPrefs.SetFloat(xKey, position.x);
        PlayerPrefs.SetFloat(yKey, position.y);
        PlayerPrefs.SetFloat(zKey, position.z);
        PlayerPrefs.Save();

        Debug.Log($"Vector3 saved: {position}");
    }

    private Vector3 GetVector3(string key, Vector3 defaultValue = default)
    {
        string xKey = key + XKey;
        string yKey = key + YKey;
        string zKey = key + ZKey;

        Vector3 position = defaultValue;

        if (PlayerPrefs.HasKey(xKey) && PlayerPrefs.HasKey(yKey) && PlayerPrefs.HasKey(zKey))
        {
            float x = PlayerPrefs.GetFloat(xKey);
            float y = PlayerPrefs.GetFloat(yKey);
            float z = PlayerPrefs.GetFloat(zKey);

            position = new Vector3(x, y, z);
        }

        return position;
    }
}