using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHandler : MonoBehaviour
{
    private string _sceneName;

    public string SceneName => _sceneName;

    public event Action SceneUnloaded;
    public event Action SceneLoaded;

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        Debug.Log("Scene Unloaded");

        SceneUnloaded?.Invoke();
    }

    void OnDisable()
    {      
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;    
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");

        _sceneName = scene.name;
        SceneLoaded?.Invoke();
    }
}
