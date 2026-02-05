
using UnityEngine;
using UnityEngine.EventSystems;

public class DontDestroyOnLoad : MonoBehaviour
{
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Check if an instance already exists
        /*GameObject[] objs = GameObject.FindObjectsOfType<DontDestroyOnLoadExample>().Concat(
                             new GameObject[] { this }).ToArray();

        // If there is more than one, destroy the duplicate
        if (objs.Length > 1)
        {
            // If there are other objects with this component, destroy this one
            // (Keep the first one found)
            if (this != objs[0])
            {
                Destroy(this.gameObject);
                return;
            }
        }*/

        // Make this object persist across scenes
        DontDestroyOnLoad(this.gameObject);


        //PlayerPrefs.DeleteAll();

      //  Debug.Log("AWAKE in " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " "+ gameObject.name);
    }

   /* void OnEnable()
    {
        // Find the active EventSystem in the current scene
        UpdateEventSystemReference();

        // Subscribe to scene load events
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        UpdateEventSystemReference();
    }*/

    void UpdateEventSystemReference()
    {
        // If you're using UI elements, ensure they're properly registered
        var eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            Debug.LogWarning("No EventSystem found in the scene!");
        }
        else
        {
            Debug.Log("Event system updated");
        }
    }

    private void Start()
    {
        //Debug.Log("Start in " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // Optional: example behavior to verify persistence
    private void Update()
    {
        // For demonstration: press L to load a new scene named "NextScene"
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("NextScene");
        }      
    }
}

