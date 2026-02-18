using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class WheeelViewSteering : MonoBehaviour
{
    public void RotateTo(Vector3 targetDirection)
    {
        Vector3 forward = targetDirection.normalized;
        Vector3 up = transform.up;

        Quaternion rotation = Quaternion.LookRotation(forward, up);
        transform.rotation = rotation;

    }
}
