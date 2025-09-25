using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(WheelMover))]
public class DrivingWheel : MonoBehaviour, IMovableWheel
{
    private WheelMover _wheelMover;
    private Rigidbody _rigidbody;
    private IDirectionChanger _directionChanger;
    //ADD CAR BODY FOR TRANSFORM Forward direction

    protected Vector3 LookDirection { get; private set; }

    private void Awake()
    {
        UseInAwake();
    }

    private void OnEnable()
    {
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged;
        }
    }

    private void OnDisable()
    {
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged -= OnDirectionChanged;
        }
    }

    public void ForwardMove(float force)
    {
        _wheelMover.ForwardMove(force, _rigidbody, LookDirection);
    }

    public void BackwardMove(float force)
    {
        _wheelMover.BackwardMove(force, _rigidbody, LookDirection);
    }

    protected virtual void UseInAwake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _wheelMover = GetComponent<WheelMover>();
        LookDirection = transform.forward;
        _directionChanger = GetComponent<IDirectionChanger>();
    }

    private void OnDirectionChanged(Vector3 direction)
    {
        LookDirection = direction;
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(LookDirection) * 20);
    }
}
