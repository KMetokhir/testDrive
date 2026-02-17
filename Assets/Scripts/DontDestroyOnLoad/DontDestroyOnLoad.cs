
using UnityEngine;
using UnityEngine.EventSystems;

public class DontDestroyOnLoad : MonoBehaviour
{
    
    private void Awake()
    {
      //  PlayerPrefs.DeleteAll();
     
        DontDestroyOnLoad(this.gameObject);       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("NextScene");
        }      
    }
}

