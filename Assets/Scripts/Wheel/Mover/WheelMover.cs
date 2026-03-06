using System;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using Zenject;

public class WheelMover : MonoBehaviour // from ground checker get the normal to surface and rotate direction in 90 degree to normal
{
    private const int ForwardDirection = 1;
    private const int BackwardDirection = -1;
    private const int StopDirection = 0;

    [SerializeField] private GroundChecker _groundChecker;

    private bool _isMoving;

    private Vector3 _direction;
    private Rigidbody _rigidbody;

    [Inject]
    private ISpeedData _speedData;

    private int _lookDirection;

    public bool IsGrounded => _groundChecker.IsGrounded();

    public event Action<float, int, float> RigidbodyMoving;

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

    public void ForwardMove(Rigidbody rigidbody, Vector3 direction)
    {
        _lookDirection = ForwardDirection;

        InitializeVAriables(rigidbody, direction * _lookDirection);
    }

    public void BackwardMove(Rigidbody rigidbody, Vector3 direction
        )
    {
        _lookDirection = BackwardDirection;

        InitializeVAriables(rigidbody, direction * _lookDirection);
    }

    public void StopMoving()
    {
        _lookDirection = StopDirection;

        _isMoving = false;
    }

    private void InitializeVAriables(Rigidbody rigidbody, Vector3 direction)
    {
        _direction = direction;
        _isMoving = true;
        _rigidbody = rigidbody ?? throw new NullReferenceException(nameof(rigidbody));
        //  _speedData = speed ?? throw new NullReferenceException(nameof(speed));
    }

    private void Move(float force, Rigidbody rigidbody, Vector3 direction)
    {
        rigidbody.AddRelativeForce(direction * force);
    }

    private void FixedUpdate()
    {

        if (_isMoving && _groundChecker.IsGrounded())
        {
            Move(_speedData.Acceleration, _rigidbody, _direction);

            RigidbodyMoving?.Invoke(_rigidbody.velocity.magnitude, _lookDirection, Time.fixedDeltaTime);
        }

        if (_isMoving && _rigidbody.velocity.z > _speedData.MaxSpeed)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _speedData.MaxSpeed);

            RigidbodyMoving?.Invoke(_rigidbody.velocity.magnitude, _lookDirection, Time.fixedDeltaTime);
        }
    }
}