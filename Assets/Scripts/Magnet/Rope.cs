using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private HingeJoint _joint;
    [SerializeField] private Transform _connectPoint;

    public void ConnectTarget(Rigidbody target, Transform targetConnectPoint)
    {
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedBody = target;

        Vector3 targetPoint = targetConnectPoint.position;

        _joint.anchor = transform.InverseTransformPoint(_connectPoint.position);
        _joint.connectedAnchor = target.transform.InverseTransformPoint(targetPoint);
    }
}