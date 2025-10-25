using System;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class SurfacePoint
{
    private Vector3 _localNormal;
    private Vector3 _localPosition;
    private Transform _owner;

    public Vector3 WordPosition => _owner.position + _owner.rotation * _localPosition;
    public Vector3 OwnerPosition => _owner.position;
    public Vector3 Normal => _owner.TransformDirection(_localNormal);

    public SurfacePoint(Vector3 position, Vector3 normal, Transform owner)
    {
        _localPosition = position;
        _localNormal = normal;

        _owner = owner ?? throw new ArgumentNullException(nameof(owner));
    }
}
