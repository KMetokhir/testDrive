
using UnityEngine;

public static class PlayerPrefsManager
{
    /*private const string WebSessionKey = "WebGL_Session_ID";*/

    private const string XKey = "X";
    private const string YKey = "Y";
    private const string ZKey = "Z";

    // Basic save
    public static void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Basic load with default value
    public static int LoadInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SaveVector3(string key, Vector3 position)
    {
        string xKey = key + XKey;
        string yKey = key + YKey;
        string zKey = key + ZKey;

        PlayerPrefs.SetFloat(xKey, position.x);
        PlayerPrefs.SetFloat(yKey, position.y);
        PlayerPrefs.SetFloat(zKey, position.z);
        PlayerPrefs.Save();

        Debug.Log($"Position saved: {position}");
    }

    public static Vector3 GetVector3(string key, Vector3 defaultValue = default)
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

   

    public static void SaveRotation(string key, Quaternion quaternion)
    {
        Vector3 euler = quaternion.eulerAngles;

        string xKey = key + XKey;// repeat
        string yKey = key + YKey; 
        string zKey = key + ZKey;

        PlayerPrefs.SetFloat(xKey, euler.x);
        PlayerPrefs.SetFloat(yKey, euler.y);
        PlayerPrefs.SetFloat(zKey, euler.z);
        PlayerPrefs.Save();
    }


    public static Quaternion GetRotation(string key, Quaternion defaultValue = default)
    {
        string xKey = key + XKey;// repeat
        string yKey = key + YKey;
        string zKey = key + ZKey;

        if (PlayerPrefs.HasKey(xKey))
        {
            float x = PlayerPrefs.GetFloat(xKey);
            float y = PlayerPrefs.GetFloat(key + "_eulerY");
            float z = PlayerPrefs.GetFloat(key + "_eulerZ");

            return Quaternion.Euler(x, y, z);
        }

        return defaultValue;
    }

}