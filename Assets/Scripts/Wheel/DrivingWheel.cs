using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class DrivingWheel : MonoBehaviour, ISphereShape, IWheel, IWheelDirection
{
    [SerializeField] private WheelView _wheelView;
    [SerializeField] private PhysicWheel _physicWheel;

    private WheelMover _wheelMover;
    private Rigidbody _rigidbody;
    private IDirectionChanger _directionChanger;

    private Suspension _suspension;

    private bool _isActivated;

    [Inject]
    private WheelMover.Factory _wheelMoverFactory;

    public float Radius => _physicWheel.Radius;
    public Transform Transform => transform;
    public Vector3 LookDirectionLocal { get; private set; }
    public Vector3 LookDirectionWorld => transform.TransformDirection(LookDirectionLocal);

    private void Awake()
    {
        UseInAwake();
    }

    private void OnEnable()
    {
        UseInEneble();
    }

    private void OnDisable()
    {
        UseInDisable();
    }

    private void FixedUpdate()
    {
        UseInFixedUpdate();
    }

    public void Activate()
    {
        if (_isActivated == false)
        {
            _isActivated = true;
            _suspension.Activate(_rigidbody);
        }
        else
        {
            throw new Exception($"Wheel {this.gameObject} activated allready");
        }
    }

    public void ForwardMove()
    {
        _wheelMover.ForwardMove(_rigidbody);
    }

    public void BackwardMove()
    {
        _wheelMover.BackwardMove(_rigidbody);
    }

    public void StopMoving()
    {
        _wheelMover.StopMoving();
    }

    protected virtual void UseInEneble()
    {
        _wheelMover.RigidbodyMoving += MoveView;
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged;
        }
    }

    protected virtual void UseInDisable()
    {
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged -= OnDirectionChanged;
        }
    }

    protected virtual void UseInAwake()
    {
        _isActivated = false;

        _rigidbody = GetComponent<Rigidbody>();
        LookDirectionLocal = transform.forward;
        _directionChanger = GetComponent<IDirectionChanger>();
        _suspension = GetComponent<Suspension>();

        GroundChecker groundChecker = GetComponent<GroundChecker>();
        _wheelMover = _wheelMoverFactory.Create(this, groundChecker);
    }

    protected virtual void UseInFixedUpdate()
    {
        _wheelMover?.FixedUpdate();
    }

    private void MoveView(float speed, int clockRotation, float deltaTime)
    {
        _wheelView.Move(Mathf.Abs(speed), clockRotation, deltaTime, transform.TransformDirection(LookDirectionLocal));
    }

    private void OnDirectionChanged(Vector3 direction)
    {
        LookDirectionLocal = direction;

        _wheelView.RotateTo(transform.TransformDirection(LookDirectionLocal));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(LookDirectionLocal) * 20);

        Gizmos.color = Color.red;

        Vector3 movedirection = _wheelMover?.MoveDirection ?? Vector3.zero;
        Gizmos.DrawLine(transform.position, transform.position + movedirection * 20);
    }
}