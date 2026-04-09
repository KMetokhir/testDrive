using UnityEngine;

[RequireComponent(typeof(HingeJoint), typeof(Rigidbody))]
public abstract class ConnectableBase : MonoBehaviour, IConnectable
{
    [SerializeField] private ConnectionPoint _connectionPoint;

    private HingeJoint _joint;
    private Rigidbody _rigidbody;

    public ConnectionPoint Point => _connectionPoint;
    public Rigidbody ConnectionRigidbody => _rigidbody;

    private void Awake()
    {
        UseInAwake();
    }

    public void ConnectTarget(IConnectable connactable, bool isAutoConnect)
    {
        _joint.autoConfigureConnectedAnchor = isAutoConnect;

        _joint.connectedBody = connactable.ConnectionRigidbody;

        _joint.anchor = transform.InverseTransformPoint(_connectionPoint.Position);

        if (isAutoConnect == false)
        {
            _joint.connectedAnchor = GetConnectedAnchor(connactable.ConnectionTransform, connactable.Point.Position);
        }
    }

    protected virtual void UseInAwake()
    {
        _joint = GetComponent<HingeJoint>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected abstract Vector3 GetConnectedAnchor(Transform target, Vector3 position);
}