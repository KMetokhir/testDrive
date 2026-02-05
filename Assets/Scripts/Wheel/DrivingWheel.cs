using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(WheelMover))]
public class DrivingWheel : MonoBehaviour, ISphereShape, IWheel
{
    // [SerializeField] private WheelSpawner _spawner;

    [SerializeField] private WheelView _wheelView;
    [SerializeField] private PhysicWheel _physicWheel;

    private WheelMover _wheelMover;
    private Rigidbody _rigidbody;
    private IDirectionChanger _directionChanger;

    private Suspension _suspension;

    private bool _isActivated;

    public float Radius => _physicWheel.Radius;
    public Transform Transform => transform;
    protected Vector3 LookDirection { get; private set; }

    private void Awake()
    {
        _isActivated = false;

        UseInAwake();

        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged; // subscribe on enable is not work after start abviously
        }

        // _spawner.NewWheelSpawned += ChangePhysicWheel;
    }

    private void OnEnable()
    {
        _wheelMover.RigidbodyMoving += MoveView;

        /*if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged;
        }*/
    }

    private void MoveView(float speed, int clockRotation, float deltaTime)
    {
        _wheelView.Move(Mathf.Abs(speed), clockRotation, deltaTime, transform.TransformDirection(LookDirection));
    }

    /* private void ChangePhysicWheel(WheelUpgrade upgrade)
     {

         if (upgrade is PhysicWheel)
         {
             _physicWheel = upgrade as PhysicWheel;
         }
         else
         {
             throw new Exception(" PhysicWheel does not implement WheelUpgrade");
         }
     }*/

    private void OnDisable()
    {
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged -= OnDirectionChanged;
        }

        // _spawner.NewWheelSpawned -= ChangePhysicWheel;
    }

    public void Activate(ICarBody carBody)
    {
        if (_isActivated == false)
        {
            _isActivated = true;
            _suspension.Activate(carBody, _rigidbody);
        }
        else
        {
            throw new Exception($"Wheel {this.gameObject} activated allready");
        }
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
        _suspension = GetComponent<Suspension>();
    }

    private void OnDirectionChanged(Vector3 direction)
    {
        LookDirection = direction;

        _wheelMover.SetDirection(direction);


        _wheelView.RotateTo(transform.TransformDirection(LookDirection));

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(LookDirection) * 20);
    }
}