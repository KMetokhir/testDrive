using UnityEngine;

public interface ICarBody: ICarDirection
{
    public Rigidbody Rigidbody { get; }
}
