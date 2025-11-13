using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRotator : MonoBehaviour
{
    public void RotateTo(Vector3 targetDirection)
    {
        if (targetDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
}
