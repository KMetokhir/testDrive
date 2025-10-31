using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(WheelMover))]
public class DrivingWheel : MonoBehaviour, ISphereShape
{
    [SerializeField] private WheelSpawner _spawner;

    private WheelMover _wheelMover;
    private Rigidbody _rigidbody;
    private IDirectionChanger _directionChanger;
    private PhysicWheel _physicWheel;

    public float Radius => _physicWheel.Radius;
    public Transform Transform => transform;
    protected Vector3 LookDirection { get; private set; }

    private void Awake()
    {
        UseInAwake(); // awake in start

        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged; // subscribe on enable is not work after start abviously
        }

        _spawner.NewWheelSpawned += ChangePhysicWheel;
    }

    private void OnEnable()
    {
        /*if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged;
        }*/
    }

    private void ChangePhysicWheel(WheelUpgrade upgrade)
    {

        if (upgrade is PhysicWheel)
        {
            _physicWheel = upgrade as PhysicWheel;
        }
        else
        {
            throw new Exception(" PhysicWheel does not implement WheelUpgrade");
        }
    }

    private void OnDisable()
    {
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged -= OnDirectionChanged;
        }

        _spawner.NewWheelSpawned -= ChangePhysicWheel;
    }

    public void ForwardMove(ISpeed speed)
    {
        _wheelMover.ForwardMove(speed, _rigidbody, LookDirection);
    }

    public void BackwardMove(ISpeed speed)
    {
        _wheelMover.BackwardMove(speed, _rigidbody, LookDirection);
    }

    public void StopMoving()
    {
        _wheelMover.StopMoving();
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
        
        _wheelMover.SetDirection(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(LookDirection) * 20);
    }
}