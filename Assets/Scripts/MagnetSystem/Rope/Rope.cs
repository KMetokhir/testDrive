using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Rope : ConnectableBase
{
    [SerializeField] private ConnectionPoint _ropeStartPoint;
    [SerializeField] private ConnectionPoint _ropeEndPoint;

    [Header("Rope Settings")]
    [SerializeField] private int _segments = 20;
    [SerializeField] private float _sagAmount = 1f;
    [SerializeField] private float _width = 0.2f;

    private RopeView _view;

    private void Update()
    {
        _view.DrawRope(_ropeStartPoint.Position, _ropeEndPoint.Position);
    }

    protected override void UseInAwake()
    {
        base.UseInAwake();

        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        _view = new RopeView(lineRenderer, _segments, _sagAmount, _width);
    }

    protected override Vector3 GetConnectedAnchor(Transform target, Vector3 position)
    {
        return target.transform.InverseTransformPoint(position);
    }
}