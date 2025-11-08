using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotaryWheel
{
    public void RotateWheel(float angle, Rotation rotation);
    public void StopRotation();
}
