using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _maxAngle;
    [SerializeField] float _ackermannMultiplier;

    public Vector3 GetRightWheelDirection(Vector3 wheelDirection, Vector3 wheelWorldDirection, float angle, Vector3 carForwardDirection)
    {
        float currentRotationAngle = CalculateAngleXZPlane(carForwardDirection, wheelWorldDirection);
        int rightRotation = 1;
        int leftRotation = -1;



        if (angle > 0)
        {

            if (Approximately(currentRotationAngle , angle * _ackermannMultiplier, 2))
            {
                return wheelDirection;
            }

            if ((Approximately(currentRotationAngle, _maxAngle * _ackermannMultiplier * rightRotation, 2) == false))
            {
                wheelDirection = Quaternion.AngleAxis(_rotationSpeed * _ackermannMultiplier * rightRotation, Vector3.up) * wheelDirection;
            }
        }

        if (angle < 0)
        {
            if (Approximately(currentRotationAngle, angle, 2))
            {
                return wheelDirection;
            }

            if ((Approximately(currentRotationAngle, _maxAngle * leftRotation, 2) == false))
            {
                wheelDirection = Quaternion.AngleAxis(_rotationSpeed * leftRotation, Vector3.up) * wheelDirection;
            }
        }

        return wheelDirection;

    }

    /*private Vector3 GetWheelDirection() { }*/

    private bool Approximately(float a, float b, int equalFactor)
    {
        return Mathf.Abs(a - b) <= equalFactor;
    }

    private float CalculateAngleXZPlane(Vector3 vectorA, Vector3 vectorB)
    {
        float y = 0;
        vectorA = new Vector3(vectorA.x, y, vectorA.z);
        vectorB = new Vector3(vectorB.x, y, vectorB.z);

        float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }
}
