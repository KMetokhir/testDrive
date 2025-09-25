using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWheelRotator : WheelRotator
{
    public override float GetMultiplier(float currentRotationAngle, float ackermannMultiplier)
    {
        return currentRotationAngle < 0 ? ackermannMultiplier : 1;
    }
}
