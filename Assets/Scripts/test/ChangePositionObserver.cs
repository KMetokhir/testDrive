using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePositionObserver : MonoBehaviour
{
    Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }
    private void Update()
    {

        if(pos!= transform.position)
        {
           /* Debug.Log("changed new pos");
            Debug.Log(transform.position);*/
            pos = transform.position;
        }
    }
}
