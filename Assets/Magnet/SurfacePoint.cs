using System;
using UnityEngine;

public class SurfacePoint
{
    public readonly Vector3 Normal;

    private Vector3 _localPosition;
    private Transform _owner;

    public Vector3 WordPosition => _owner.position + _owner.rotation * _localPosition;

    public SurfacePoint(Vector3 position, Vector3 normal, Transform owner)
    {
        _localPosition = position;
        Normal = normal;

        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }
}
