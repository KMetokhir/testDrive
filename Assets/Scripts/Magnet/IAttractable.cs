using UnityEngine;

public interface IAttractable : ICollectable
{
    public bool IsActive { get; }
    public Transform Transform { get; }   

    public void Deactivate();
}