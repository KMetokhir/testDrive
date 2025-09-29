using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(WheelMover), typeof(SphereCollider))]
public class DrivingWheel : MonoBehaviour, ISphereShape
{
    private WheelMover _wheelMover;
    private Rigidbody _rigidbody;
    private IDirectionChanger _directionChanger;

    public float Radius { get; private set; }
    public Transform Transform => transform;
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

    public void StopMoving()
    {
        _wheelMover.StopMoving();
    }

    protected virtual void UseInAwake()
    {
        Radius = GetComponent<SphereCollider>().radius * transform.localScale.y;

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