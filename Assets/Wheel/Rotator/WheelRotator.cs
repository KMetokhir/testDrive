using System;
using UnityEngine;

public abstract class WheelRotator : MonoBehaviour, IDirectionChanger
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _ackermannMultiplier;

    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private Transform _carBodyTransform;

    private bool _isRotating;
    private float _targetAngle;
    private Vector3 _wheelDirection;

    public event Action<Vector3> DirectionChanged;

    private void Awake()
    {
        _isRotating = false;
        _wheelDirection = _carBodyTransform.forward;
        _targetAngle = 0;
    }

    // add check method and use rotation method only in fixedUpdate
    public void Rotate(Vector3 wheelDirection, float angle)
    {
        Vector3 wheelWorldDirection = _wheelTransform.TransformDirection(wheelDirection);
        Vector3 carForwardDirection = _carBodyTransform.forward;

        float currentRotationAngle = CalculateAngle(carForwardDirection, wheelWorldDirection, _carBodyTransform.up);

      //  Debug.Log(currentRotationAngle);

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
            Rotate(_wheelDirection, _targetAngle);
        }
    }

    private bool Approximately(float a, float b, float equalFactor)
    {
        return Mathf.Abs(a - b) <= Mathf.Abs(equalFactor);
    }

    private float CalculateAngle(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
    {
        float y = 0;
        vectorA = new Vector3(vectorA.x, y, vectorA.z);
        vectorB = new Vector3(vectorB.x, y, vectorB.z);

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }
}