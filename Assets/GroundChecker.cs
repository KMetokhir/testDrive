using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _rayLengthOffset;
    [SerializeField] private int _rayDegreeOffset;
    [SerializeField] private int _countOfPositiveRays;

    private ISphereShape _owner;

    private List<Ray> _rays;

    private void Awake()
    {
        _owner = GetComponent<ISphereShape>();
    }

    public bool IsGrounded()
    {
        float rayLength = _owner.Radius + _rayLengthOffset;
        // Ray ray = new Ray(_owner.Transform.position, -_owner.Transform.up);

        List<Ray> rays = GetGroundCheckRays(_owner.Transform.position, _countOfPositiveRays, _rayDegreeOffset);
        _rays = rays;

        bool isGrounded = false;

        foreach (Ray ray in rays)
        {
            if (Physics.Raycast(ray, rayLength, _groundLayer))
            {
                isGrounded = true;
            }
        }

        return isGrounded; //Physics.Raycast(position, Vector3.down, out RaycastHit hit, rayLength, _groundLayer);
    }

    private List<Ray> GetGroundCheckRays(Vector3 origin, int positioveRaysCount, int degreeOffset)
    {
        if (positioveRaysCount <= 0)
        {
            throw new NullReferenceException(nameof(positioveRaysCount));
        }

        Transform ownerTransform = _owner.Transform;

        List<Ray> rays = new List<Ray>();

        Vector3 startDirection = -ownerTransform.up;
        Vector3 rayDirection;
        int positiveRotation = 1;
        int negativeRotation = -1;

        for (int i = 0; i <= positioveRaysCount; i++)
        {
            rayDirection = Quaternion.AngleAxis(degreeOffset * i * positiveRotation, ownerTransform.right) * startDirection;
            rays.Add(new Ray(origin, rayDirection));

            if (i != 0)
            {
                rayDirection = Quaternion.AngleAxis(degreeOffset * i * negativeRotation, ownerTransform.right) * startDirection;
                rays.Add(new Ray(origin, rayDirection));// ADD ROTATE RAY METHOD
            }
        }

        return rays;

    }

    private void OnDrawGizmos()
    {
        if (_rays != null)
        {
            Gizmos.color = Color.red;

           // List<Ray> rays = GetGroundCheckRays(_owner.Transform.position, 5, 15);

            foreach (Ray ray in _rays)
            {
                Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * (_rayLengthOffset + _owner.Radius));
            }

            // Gizmos.DrawLine(transform.position, transform.position + -_owner.Transform.up * (_rayLengthOffset + _owner.Radius));
        }

    }



    // isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, groundLayer);

}
