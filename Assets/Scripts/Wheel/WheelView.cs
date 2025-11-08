using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class WheelView : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    
    public void Rotatte(float rotation)
    {       
        transform.Rotate(Vector3.right, rotation);
    }

    private void Update()
    {
        transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.World);
    }
}
