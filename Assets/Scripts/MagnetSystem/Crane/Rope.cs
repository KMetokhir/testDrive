using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(HingeJoint), typeof(LineRenderer))]
public class Rope : MonoBehaviour, IConnectable
{
    [SerializeField] private ConnectionPoint _magnetConnectionPoint;
    [SerializeField] private ConnectionPoint _ropeStartPoint;
    [SerializeField] private ConnectionPoint _ropeEndConnectionPoint;

    [Header("Rope Settings")]
    [SerializeField] private int segments = 20;
    [SerializeField] private float sagAmount = 1f;
    [SerializeField] private float width = 0.2f;

    private LineRenderer _lineRenderer;

    private Rigidbody _rigRigidbody;
    private HingeJoint _joint;

    public Transform ConnectionTransform => _ropeEndConnectionPoint.ConnectionTransform;

    private void Awake()
    {
        _joint = GetComponent<HingeJoint>();
        _rigRigidbody = GetComponent<Rigidbody>();

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = segments;
        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
    }

    private void Update()
    {
        DrawRope();
    }

    public void ConnectTarget(Rigidbody target, IConnectable connactable)
    {
        _joint.autoConfigureConnectedAnchor = false;
        _joint.connectedBody = target;

        Vector3 worldOffset = connactable.ConnectionTransform.position - _magnetConnectionPoint.ConnectionTransform.position;

        _joint.anchor = transform.InverseTransformPoint(_magnetConnectionPoint.ConnectionTransform.position);

        _joint.connectedAnchor = target.transform.InverseTransformPoint(connactable.ConnectionTransform.position);
    }

    private void DrawRope()
    {
        Vector3 startPoint = _ropeStartPoint.ConnectionTransform.position;
        Vector3 endPoint = _ropeEndConnectionPoint.ConnectionTransform.position;

        Vector3 midPont = (startPoint + endPoint) / 2f;

        Vector3 sagPoint = midPont + Vector3.down * sagAmount;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 point = GetBezierPoint(t, startPoint, sagPoint, endPoint);
            _lineRenderer.SetPosition(i, point);
        }
    }

    private Vector3 GetBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        return Mathf.Pow(1 - t, 2) * p0 +
               2 * (1 - t) * t * p1 +
               Mathf.Pow(t, 2) * p2;
    }
}