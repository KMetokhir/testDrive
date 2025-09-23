using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wheel : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private Vector3 _lookDirection;
    private WheelRotator _rotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lookDirection = transform.forward;
    }

    public void ForwardMove(float force)
    {
        Vector3 direction = _lookDirection;
        Move(force, direction);
    }

    public void BackwardMove(float force)
    {
        Vector3 direction = -_lookDirection;
        Move(force, direction);

    }

    private void Move(float force, Vector3 direction)
    {
        _rigidbody.AddRelativeForce(direction * force);
    }
}
