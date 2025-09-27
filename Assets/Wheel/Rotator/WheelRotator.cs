using System;
using UnityEngine;
using UnityEngine.U2D;

public abstract class WheelRotator : MonoBehaviour, IDirectionChanger
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _maxAngle;
    [SerializeField] float _ackermannMultiplier;

    [SerializeField] Transform _wheelTransform;
    [SerializeField] Transform _carBodyTransform;

    private bool _isRotating;
    private float _targetAngle;
    private Vector3 _wheelDirection; /////////   

    public event Action<Vector3> DirectionChanged;

    private void Awake()
    {
        _isRotating = false;
        _wheelDirection = _carBodyTransform.forward;
        _targetAngle = 0;
    }

    public void RotateWheel(Vector3 wheelDirection, float angle)
    {
        Vector3 wheelWorldDirection = _wheelTransform.TransformDirection(wheelDirection);
        Vector3 carForwardDirection = _carBodyTransform.forward;

        float currentRotationAngle = CalculateAngleXZPlane(carForwardDirection, wheelWorldDirection);

        float multiplier = GetMultiplier(currentRotationAngle, _ackermannMultiplier);
        int clockRotation = (int)Mathf.Sign(angle * multiplier - currentRotationAngle);

        if (Approximately(currentRotationAngle, angle * multiplier, _rotationSpeed))
        {
            _isRotating = false;
            return;
        }
        else
        {
            _isRotating = true;
        }

        if ((Approximately(currentRotationAngle, _maxAngle * multiplier * clockRotation, _rotationSpeed) == false))
        {
            _isRotating = true;

            _wheelDirection = wheelDirection;
            _targetAngle = angle;

            _wheelDirection = Quaternion.AngleAxis(_rotationSpeed * multiplier * clockRotation, Vector3.up) * _wheelDirection;
            DirectionChanged?.Invoke(_wheelDirection);
        }
        else
        {
            _isRotating = false;
        }
    }

    public void StopRotating()
    {
        _isRotating = false;
    }

    public abstract float GetMultiplier(float currentRotationAngle, float ackermanMultiplier);

    private void FixedUpdate()
    {
        if (_isRotating)
        {
            RotateWheel(_wheelDirection, _targetAngle);
        }
    }


    private bool Approximately(float a, float b, float equalFactor)
    {
        return Mathf.Abs(a - b) <= Mathf.Abs(equalFactor);
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
