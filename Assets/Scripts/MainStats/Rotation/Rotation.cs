using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _ackermannMultiplier;

    public float RotationSpeed => _rotationSpeed;
    public float MaxAngle => _maxAngle;
    public float AckermanMultiplier => _ackermannMultiplier;
}
