using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _rayLengthOffset = 0.1f;
    [SerializeField] private int _rayDegreeOffset = 15;
    [SerializeField] private int _countOfPositiveRays = 2;

    private IWheelDirection _wheelDirection;
    private ISphereShape _wheelOwner;
    private List<Ray> _rays;

    public bool IsGrounded {  get; private set; }
    public Vector3 GroundNormal { get; private set; }

    private void Start()
    {
        GroundNormal = Vector3.up;

        _wheelDirection = GetComponent<IWheelDirection>();
        _wheelOwner = GetComponent<ISphereShape>();
    }

    private void FixedUpdate()
    {
        GroundeCheck();
    }

    public void  GroundeCheck()
    {
        float rayLength = _wheelOwner.Radius + _rayLengthOffset;

        List<Ray> rays = GetGroundCheckRays(
            _wheelOwner.Transform.position,
            _countOfPositiveRays,
            _rayDegreeOffset
        );

        _rays = rays;

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, rayLength, _groundLayer))
            {
                GroundNormal = hit.normal;
                IsGrounded =  true;

                return;
            }
        }

        GroundNormal = Vector3.up;
        IsGrounded = false;
    }

    private List<Ray> GetGroundCheckRays(Vector3 origin, int raysCount, int degreeOffset)
    {
        if (raysCount <= 0)
            throw new ArgumentException(nameof(raysCount));

        List<Ray> rays = new List<Ray>();

        Vector3 up = _wheelDirection.UpDirectionLocal;
        Vector3 forward = transform.TransformDirection(_wheelDirection.LookDirectionLocal).normalized;
        Vector3 right = Vector3.Cross(up, forward).normalized;

        Vector3 startDirection = -up;

        for (int i = 0; i <= raysCount; i++)
        {
            float angle = degreeOffset * i;

            Vector3 dirForward = Quaternion.AngleAxis(angle, right) * startDirection;
            rays.Add(new Ray(origin, dirForward));

            if (i != 0)
            {
                Vector3 dirBackward = Quaternion.AngleAxis(-angle, right) * startDirection;
                rays.Add(new Ray(origin, dirBackward));
            }
        }

        return rays;
    }

    private void OnDrawGizmos()
    {
        if (_rays == null || _wheelOwner == null)
            return;

        Gizmos.color = Color.red;

        float rayLength = _rayLengthOffset + _wheelOwner.Radius;

        foreach (Ray ray in _rays)
        {
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * rayLength);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(GroundNormal) * 20);

    }
}