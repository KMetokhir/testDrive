using System;
using UnityEngine;
using Zenject;

public class WheelMover
{
    private const float StopVelocity = 0.01f;

    private GroundChecker _groundChecker;

    private const int ForwardWay = 1;
    private const int BackwardWay = -1;
    private const int StopWay = 0;

    private bool _isMoving;
    private Rigidbody _rigidbody;
    private int _lookWay;

    private Vector3 _moveDirection;
    private bool _isDirectionChanged;

    private readonly IWheelDirection _whellDirection;
    private ISpeedData _speedData;

    public Vector3 MoveDirection => _moveDirection; // test

    public event Action<float, int, float> RigidbodyMoving;
    private Vector3 _lookDirectionWorld => _whellDirection.LookDirectionWorld * _lookWay;

    public WheelMover(IWheelDirection whellDirection, GroundChecker groundChecker, ISpeedData speedData)
    {
        _whellDirection = whellDirection;
        _groundChecker = groundChecker;
        _speedData = speedData;
        _isMoving = false;

    }

    public void FixedUpdate()
    {
        if (_rigidbody == null)
            return;

        if (_isMoving && _groundChecker.IsGrounded)
        {
            if (_isDirectionChanged)
            {
                if (TryStop(_speedData.ChangeDirectionSpeed))
                {
                    _isDirectionChanged = false;
                }
            }
            else
            {
                Move(_speedData.Acceleration);
            }

            RigidbodyMoving?.Invoke(
                _rigidbody.velocity.magnitude,
                _lookWay,
                Time.fixedDeltaTime
            );
        }

        if (_isMoving && _rigidbody.velocity.magnitude > _speedData.MaxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _speedData.MaxSpeed;

            RigidbodyMoving?.Invoke(
                _rigidbody.velocity.magnitude,
                _lookWay,
                Time.fixedDeltaTime
            );
        }

        if (_isMoving == false)
        {
            TryStop(StopVelocity);
        }
    }

    public void ForwardMove(Rigidbody rigidbody)
    {
        if (_lookWay == ForwardWay)
        {
            return;
        }
        else if (_lookWay != StopWay)
        {
            _isDirectionChanged = true;
        }

        _lookWay = ForwardWay;
        InitializeVariables(rigidbody);
    }

    public void BackwardMove(Rigidbody rigidbody)
    {
        if (_lookWay == BackwardWay)
        {
            return;
        }

        _lookWay = BackwardWay;
        _isDirectionChanged = true;
        InitializeVariables(rigidbody);
    }

    public void StopMoving()
    {
        _lookWay = StopWay;
        _isMoving = false;
    }

    private void InitializeVariables(Rigidbody rigidbody)
    {
        _rigidbody = rigidbody ?? throw new ArgumentNullException(nameof(rigidbody));
        _isMoving = true;
    }

    private bool TryStop(float stopVelocity)
    {
        bool isStoped = false;

        if (_rigidbody.velocity.magnitude > stopVelocity)
        {
            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, Vector3.zero, _speedData.StopSpeed * Time.fixedDeltaTime);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            isStoped = true;
        }

        return isStoped;
    }

    private void Move(float force)
    {
        Vector3 groundNormal = _groundChecker.GroundNormal;

        _moveDirection = Vector3
           .ProjectOnPlane(_lookDirectionWorld, groundNormal)
           .normalized;

        _rigidbody.AddForce(_moveDirection * force);
    }

    public class Factory : PlaceholderFactory<IWheelDirection, GroundChecker, WheelMover>
    {
    }
}