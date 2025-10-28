using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PhysicWheel : WheelUpgrade
{
    private SphereCollider _collider;

    public float Radius => _collider.radius * transform.localScale.y;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }
}
