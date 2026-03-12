using UnityEngine;

public interface IAttractable : ICollectable

{
    public bool IsActive { get; }
    public bool IsPossibleToCollect { get; }

    public void TransitToStore();
}