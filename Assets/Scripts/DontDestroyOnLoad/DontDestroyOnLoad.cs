
using UnityEngine;
using UnityEngine.EventSystems;

public class DontDestroyOnLoad : MonoBehaviour
{
    
    private void Awake()
    {
     
        DontDestroyOnLoad(this.gameObject);       
    }

  

  /*  void UpdateEventSystemReference()
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
    }*/


    private void Update()
    {
        // For demonstration: press L to load a new scene named "NextScene"
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("NextScene");
        }      
    }
}

