using System;
using TMPro;
using UnityEngine;

public class TEstIntPrefs : MonoBehaviour
{
    public int i;
    public string key;

   [SerializeField] private TMP_Text old;
    [SerializeField] private TMP_Text newVar;

    public void Show(uint value, TMP_Text textMesh)
    {
        textMesh.text = value.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        key = nameof(i);
        // i = PlayerPrefsIntManager.LoadVariable(key);
        i = PlayerPrefsIntManager.LoadInt(key);
        Debug.Log("start " + i);
        Show((uint)i, old);

        i=i +1;
        //PlayerPrefsIntManager.SaveVariable(key, i);
        PlayerPrefsIntManager.SaveInt(key, i);
        Debug.Log("Load "+ i);

        // i = PlayerPrefsIntManager.LoadVariable(key);
        i = PlayerPrefsIntManager.LoadInt(key);
        Debug.Log("after load " + i);
        Show((uint)i, newVar);
    }    
}
