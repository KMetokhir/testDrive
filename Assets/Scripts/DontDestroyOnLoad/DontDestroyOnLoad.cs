using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // test
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("NextScene");
        }
    }
}