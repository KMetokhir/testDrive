using System;
using UnityEngine;

public abstract class RotaryWheel : DrivingWheel, IRotaryWheel
{
    public abstract void RotateWheel(float angle);
    public abstract void StopRotation();

    public abstract event Action<Vector3> DirectionChanged;
}
