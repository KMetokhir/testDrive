using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(WheelMover))]
public class DrivingWheel : MonoBehaviour, ISphereShape
{
    private WheelMover _wheelMover;
    private Rigidbody _rigidbody;
    private IDirectionChanger _directionChanger;
    private PhysicWheel _physicWheel;

    public float Radius => _physicWheel.Radius;
    public Transform Transform => transform;
    protected Vector3 LookDirection { get; private set; }

    private void Start()
    {
        UseInAwake(); // awake in start

        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged; // subscribe on enable is not work after start abviously
        }
    }

    private void OnEnable()
    {
        /*if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged += OnDirectionChanged;
        }*/
    }

    private void OnDisable()
    {
        if (_directionChanger != null)
        {
            _directionChanger.DirectionChanged -= OnDirectionChanged;
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
        // Radius = GetComponentInChildren<SphereCollider>().radius * transform.localScale.y;

        _physicWheel = GetComponentInChildren<PhysicWheel>();

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