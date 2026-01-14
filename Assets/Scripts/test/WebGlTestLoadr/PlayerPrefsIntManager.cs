
using UnityEngine;

public static class PlayerPrefsIntManager
{
    /*private const string WebSessionKey = "WebGL_Session_ID";*/


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

   /* private static void CreateNewSession()
    {
        string sessionId = System.Guid.NewGuid().ToString().Substring(0, 8); //tmp
        PlayerPrefs.SetString(WebSessionKey, sessionId);
        PlayerPrefs.Save();
        Debug.Log($"Created new WebGL session: {sessionId}");
    }*/

   /* public static int LoadVariable(string playerPrefsKey, int defaultValue = 0)
    {

        // First check if we have a session ID
        if (!PlayerPrefs.HasKey(WebSessionKey))
        {
            CreateNewSession();
        }

        // Check if this session has saved variable
        string sessionId = PlayerPrefs.GetString(WebSessionKey);
        string sessionKey = $"{playerPrefsKey}_{sessionId}";

        Debug.Log(sessionKey);

        if (PlayerPrefs.HasKey(WebSessionKey))
        {
            Debug.Log($"has key " + PlayerPrefs.GetInt(playerPrefsKey, defaultValue));
        }


        return PlayerPrefs.GetInt(playerPrefsKey, defaultValue);
    }

    public static void SaveVariable(string playerPrefsKey, int value)
    {
        if (PlayerPrefs.HasKey(WebSessionKey) == false)
        {
            throw new System.Exception("Session Not created, load first");
        }

        string sessionId = PlayerPrefs.GetString(WebSessionKey);
        string sessionKey = $"{playerPrefsKey}_{sessionId}";

        PlayerPrefs.SetInt(sessionKey, value);
        PlayerPrefs.Save();

        Debug.Log($"has key " + PlayerPrefs.GetInt(playerPrefsKey, -1));

        //_loadedVariable = variable;
        Debug.Log($"Saved variable for session  {playerPrefsKey}_{sessionId}  {value}");
    }*/
}