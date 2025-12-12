using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WebGLSessionLoader : MonoBehaviour
{
    [SerializeField] private TestSO _defaultVariable;
    [SerializeField] private string _playerPrefsKey = "WebGL_ScriptableVar";

    [SerializeField]  private TestSO _loadedVariable;

    private void OnEnable()
    {
    
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveVariableForSession(_loadedVariable);

    }

private void Start()
    {
        // Initial load
        LoadVariableForSession();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reload when new scene loads
       // LoadVariableForSession();
    }

    private void LoadVariableForSession()
    {
        


        // First check if we have a session ID
        if (!PlayerPrefs.HasKey("WebGL_Session_ID"))
        {
            CreateNewSession();
        }

        // Check if this session has saved variable
        string sessionId = PlayerPrefs.GetString("WebGL_Session_ID");
        string sessionKey = $"{_playerPrefsKey}_{sessionId}";

        Debug.Log(sessionKey);

        if (PlayerPrefs.HasKey(sessionKey))
        {
            string savedPath = PlayerPrefs.GetString(sessionKey);
            _loadedVariable = Resources.Load<TestSO>(savedPath);

            Debug.Log("Get Saved PAth " + savedPath);

            if (_loadedVariable != null)
            {
                Debug.Log($"Loaded variable from session {sessionId}: {_loadedVariable.name}");
                return;
            }
        }

        // Use default
        _loadedVariable = _defaultVariable;
        Debug.Log("Using default variable for new session");
    }

    private void CreateNewSession()
    {
        string sessionId = System.Guid.NewGuid().ToString().Substring(0, 8);
        PlayerPrefs.SetString("WebGL_Session_ID", sessionId);
        PlayerPrefs.Save();
        Debug.Log($"Created new WebGL session: {sessionId}");
    }

    public void SaveVariableForSession(TestSO variable)
    {
        if (variable == null)
            return;

        string sessionId = PlayerPrefs.GetString("WebGL_Session_ID");
        string sessionKey = $"{_playerPrefsKey}_{sessionId}";

        // Get Resources path (you need to implement this based on your project structure)
        string resourcePath = GetResourcePath(variable);

        PlayerPrefs.SetString(sessionKey, resourcePath);
        PlayerPrefs.Save();

        _loadedVariable = variable;
        Debug.Log($"Saved variable for session {sessionId}: {variable.name}");
    }

    public string GetResourcePath(TestSO variable)
    {
        string assetPath = UnityEditor.AssetDatabase.GetAssetPath(variable);
        string returnPath;

        // Convert to Resources path
        if (assetPath.Contains("/Resources/"))
        {
            int resourcesIndex = assetPath.LastIndexOf("/Resources/") + 11;
            string resourcesRelativePath = assetPath.Substring(resourcesIndex);

            // Remove file extension
            returnPath = System.IO.Path.ChangeExtension(resourcesRelativePath, null);
        }
        else
        {
            returnPath = string.Empty;

            throw new System.Exception("Scriptable object " + nameof(variable) + " not in Resources folder");
        }

        return returnPath;
    }

/*private string GetResourcePath(TestSO variable)
    {
        // Simple implementation - assumes variable.name matches Resources path
        // You might need a more robust mapping system
        return $"ScriptableObjects/{variable.name}";
    }*/

    // Property to access the loaded variable
    public TestSO LoadedVariable => _loadedVariable;

   
}
