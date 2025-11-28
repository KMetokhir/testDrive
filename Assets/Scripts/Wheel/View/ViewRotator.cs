using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRotator : MonoBehaviour
{
    public void RotateTo(Vector3 targetDirection)
    {
        if (targetDirection != Vector3.zero)
        {
            Vector3 direction  = new Vector3 (targetDirection.x, targetDirection.y, targetDirection.z); // to model
            transform.rotation = Quaternion.LookRotation(direction);        

        }
    }
}
