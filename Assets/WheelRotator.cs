using System;
using UnityEngine;
using UnityEngine.U2D;

public class WheelRotator : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _maxAngle;
    [SerializeField] float _ackermannMultiplier;

    [SerializeField] Transform _wheelTransform;
    [SerializeField] Transform _carBodyTransform;

    private bool _isRotating;
    private float _targetAngle;
    public Vector3 _wheelDirection; /////////

    // private Vector3 _wheelLookDirection;

    public event Action<Vector3> DirectionChanged;


    private void Awake()
    {
        _isRotating = false;
        _wheelDirection = _carBodyTransform.forward;
    }

    public void RotateRightWheel(Vector3 wheelDirection, float angle)
    {
        Vector3 wheelWorldDirection = _wheelTransform.TransformDirection(wheelDirection);
        Vector3 carForwardDirection = _carBodyTransform.forward;

        float currentRotationAngle = CalculateAngleXZPlane(carForwardDirection, wheelWorldDirection);

        Debug.Log("Start angle " + currentRotationAngle);

        int clockRotation = (int)Mathf.Sign(angle - currentRotationAngle);

        float multiplier = currentRotationAngle > 0 ? _ackermannMultiplier : 1;

        if (Approximately(currentRotationAngle, angle * multiplier, 2))
        {
            _isRotating = false;
            return;
        }
        else
        {
            _wheelDirection = wheelDirection;
            _targetAngle = angle;
            _isRotating = true;
        }

        if ((Approximately(currentRotationAngle, _maxAngle * multiplier * clockRotation, 2) == false))
        {
            _wheelDirection = Quaternion.AngleAxis(_rotationSpeed * multiplier * clockRotation, Vector3.up) * _wheelDirection;            
            DirectionChanged?.Invoke(_wheelDirection);

        }
        else
        {
            _isRotating = false;
        }

        Vector3 wheelWorldDirection2 = _wheelTransform.TransformDirection(_wheelDirection);
        Vector3 carForwardDirection2 = _carBodyTransform.forward;

        float currentRotationAngle2 = CalculateAngleXZPlane(carForwardDirection2, wheelWorldDirection2);

        Debug.Log("End angle " + currentRotationAngle2);
    }

    private void FixedUpdate()
    {
        if (_isRotating)
        {
            RotateRightWheel(_wheelDirection, _targetAngle);
        }
    }

   /* public Vector3 GetRightWheelDirection(Vector3 wheelDirection, Vector3 wheelWorldDirection, float angle, Vector3 carForwardDirection)
    {
        float currentRotationAngle = CalculateAngleXZPlane(carForwardDirection, wheelWorldDirection);
        int rightRotation = 1;
        int leftRotation = -1;

        if (angle > 0)
        {

            if (Approximately(currentRotationAngle, angle * _ackermannMultiplier, 2))
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

    }*/

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
