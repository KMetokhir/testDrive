using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reqaure component rptator
public abstract class RotaryWheel : DrivingWheel
{
    private WheelRotator _rotator;

    public void RotateWheel(float angle)
    {
        _rotator.RotateWheel(LookDirection, angle);
    }

    public void StopRotation()
    {
        _rotator.StopRotating();
    }

    protected override void UseInAwake()
    {
        _rotator = GetComponent<WheelRotator>();
        base.UseInAwake();
    }
}
