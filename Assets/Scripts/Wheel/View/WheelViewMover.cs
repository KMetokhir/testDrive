using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelViewMover : MonoBehaviour
{
    public void Move( float speed, int direction, float deltaTime)
    {

        // transform.Rotate(transform.InverseTransformDirection(axis), rotationSpeedModifier * speed * direction * deltaTime);

        /* axis = transform.rotation * transform.InverseTransformDirection(axis);

         transform.Rotate(transform.InverseTransformDirection(axis), rotationSpeedModifier * speed * direction * deltaTime);*/

        transform.Rotate(Vector3.right, speed * direction * deltaTime, Space.Self);        
    }
}
