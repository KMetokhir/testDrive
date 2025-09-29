using System;
using UnityEngine;

public class WheelMover : MonoBehaviour
{
    private const int ForwardDirection = 1;
    private const int BackwardDirection = -1;

    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private float _maxSpeed;

    private bool _isMoving;

    private Vector3 _direction;
    private Rigidbody _rigidbody;

    private float _force;

    private int _lookDirection;

    private void Awake()
    {
        _isMoving = false;
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction == null)
        {
            throw new ArgumentNullException(nameof(direction));
        }

        _direction = direction * _lookDirection;

    }

    public void ForwardMove(float force, Rigidbody rigidbody, Vector3 direction)
    {
        _lookDirection = ForwardDirection;

        InitializeVAriables(force, rigidbody, direction * _lookDirection);
    }

    public void BackwardMove(float force, Rigidbody rigidbody, Vector3 direction
        )
    {
        _lookDirection = BackwardDirection;

        InitializeVAriables(force, rigidbody, direction * _lookDirection);
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private void InitializeVAriables(float force, Rigidbody rigidbody, Vector3 direction)
    {
        _direction = direction;
        _isMoving = true;
        _rigidbody = rigidbody;
        _force = force;
    }

    private void Move(float force, Rigidbody rigidbody, Vector3 direction)
    {
        rigidbody.AddRelativeForce(direction * force);
    }

    private void FixedUpdate()
    {
        if (_isMoving && _groundChecker.IsGrounded())
        {
            Move(_force, _rigidbody, _direction);
        }

        if (_isMoving && _rigidbody.velocity.z > _maxSpeed)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _maxSpeed);
        }
    }
}