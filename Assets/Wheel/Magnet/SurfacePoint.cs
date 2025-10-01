using System;
using UnityEngine;

public class SurfacePoint
{
    public readonly Vector3 Normal;

    private Vector3 _position;
    private Transform _owner;

    public Vector3 WordPosition => _position + _owner.position;

    public SurfacePoint(Vector3 position, Vector3 normal, Transform owner)
    {
        _position = position;
        Normal = normal;

        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }
}
