using UnityEngine;

public interface ICarBody : ICarDirection
{
    public Rigidbody Rigidbody { get; }
    public Transform Transform => Rigidbody?.transform;
}