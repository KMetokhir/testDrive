using UnityEngine;

public interface IConnectable
{
    public ConnectionPoint Point { get; }

    public Rigidbody ConnectionRigidbody { get; }
    public Transform ConnectionTransform => ConnectionRigidbody.transform;
}
