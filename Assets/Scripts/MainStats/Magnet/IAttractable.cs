using System;
using UnityEngine;

public interface IAttractable : ICollectable
{
    public bool IsActive { get; }
    public Transform Transform { get; }

    /*public event Action<Vector3> PositionChanged;*/

    public void Deactivate();
}