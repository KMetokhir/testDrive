using System;
using UnityEngine;

public class WheelMover : MonoBehaviour // from ground checker get the normal to surface and rotate direction in 90 degree to normal
{
    private const int ForwardDirection = 1;
    private const int BackwardDirection = -1;

    [SerializeField] private GroundChecker _groundChecker;  

    private bool _isMoving;

    private Vector3 _direction;
    private Rigidbody _rigidbody;

    private ISpeed _speed;

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

    public void ForwardMove(ISpeed speed, Rigidbody rigidbody, Vector3 direction)
    {
        _lookDirection = ForwardDirection;

        InitializeVAriables(speed, rigidbody, direction * _lookDirection);
    }

    public void BackwardMove(ISpeed speed, Rigidbody rigidbody, Vector3 direction
        )
    {
        _lookDirection = BackwardDirection;

        InitializeVAriables(speed, rigidbody, direction * _lookDirection);
    }

    public void StopMoving()
    {
        _isMoving = false;
    }

    private void InitializeVAriables(ISpeed speed, Rigidbody rigidbody, Vector3 direction)
    {
        _direction = direction;
        _isMoving = true;
        _rigidbody = rigidbody ?? throw new NullReferenceException(nameof(rigidbody));
        _speed = speed ?? throw new NullReferenceException(nameof(speed));
    }

    private void Move(float force, Rigidbody rigidbody, Vector3 direction)
    {
        rigidbody.AddRelativeForce(direction * force);
    }

    private void FixedUpdate()
    {
        if (_isMoving && _groundChecker.IsGrounded())
        {
            Move(_speed.Value, _rigidbody, _direction);
        }

        if (_isMoving && _rigidbody.velocity.z > _speed.MaxSpeed)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _speed.MaxSpeed);
        }
    }
}