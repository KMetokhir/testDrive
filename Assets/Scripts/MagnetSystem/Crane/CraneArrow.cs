using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HingeJoint))]
public class CraneArrow : MonoBehaviour
{
    [SerializeField] private ConnectionPoint _connectionPoint;

    private HingeJoint _joint;

    public Transform ConnectionTransform => _connectionPoint.ConnectionTransform;

    private void Awake()
    {
        _joint = GetComponent<HingeJoint>();
    }

    public void ConnectTarget(Rigidbody target)
    {
        _joint.autoConfigureConnectedAnchor = true;
        _joint.connectedBody = target;

        _joint.anchor = transform.InverseTransformPoint(_connectionPoint.ConnectionTransform.position);
    }
}
