using System;
using UnityEngine;

public interface IDirectionChanger
{
    public event Action<Vector3> DirectionChanged;
}