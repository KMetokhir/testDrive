using System;
using UnityEngine;

public abstract class WheelRotator : MonoBehaviour, IDirectionChanger
{
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private Transform _carBodyTransform;

    private Rotation _rotation;

    private bool _isRotating;
    private float _targetAngle;
    private Vector3 _wheelDirection;
    private float _multiplier;
    private int _clockRotation;

    public event Action<Vector3> DirectionChanged;

    private void Awake()
    {
        _isRotating = false;
        _wheelDirection = _carBodyTransform.forward;
        _targetAngle = 0;

        DirectionChanged?.Invoke(_wheelDirection);
    }

    // rotate - incorrrect naming
    public void Rotate(Vector3 wheelDirection, float angle, Rotation rotation)
    {
        _rotation = rotation;

        Vector3 wheelWorldDirection = _wheelTransform.TransformDirection(wheelDirection);
        Vector3 carForwardDirection = _carBodyTransform.forward;

        float currentRotationAngle = CalculateAngle(carForwardDirection, wheelWorldDirection, _carBodyTransform.up);

       // Debug.Log(angle + " angle in "/* + currentRotationAngle + " " + gameObject.name*/);

        _multiplier = GetMultiplier(currentRotationAngle, _rotation.AckermanMultiplier);

        _clockRotation = (int)Mathf.Sign(angle * _multiplier - currentRotationAngle);

        _wheelDirection = wheelDirection;
        _targetAngle = angle;

        if (Approximately(currentRotationAngle, angle * _multiplier, _rotation.RotationSpeed * _multiplier))
        {
            _isRotating = false;
            return;
        }
        else
        {
            _isRotating = true;
        }

        if ((Approximately(currentRotationAngle, _rotation.MaxAngle * _multiplier * _clockRotation, _rotation.RotationSpeed * _multiplier)))
        {
            _isRotating = false;

            return;
        }
        else
        {
            _isRotating = true;
        }


        if (Mathf.Abs(currentRotationAngle) > _rotation.MaxAngle * _multiplier)
        {
            _isRotating = true;
            _clockRotation = (int)Mathf.Sign(_rotation.MaxAngle * _multiplier - currentRotationAngle);
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
            RotateWheelDirection();
            Rotate(_wheelDirection, _targetAngle, _rotation);
        }
    }

    private void RotateWheelDirection()
    {
        _wheelDirection = Quaternion.AngleAxis(_rotation.RotationSpeed * _multiplier * _clockRotation, Vector3.up) * _wheelDirection;
        DirectionChanged?.Invoke(_wheelDirection);
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