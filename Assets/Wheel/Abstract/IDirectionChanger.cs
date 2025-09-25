using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirectionChanger
{
    public event Action<Vector3> DirectionChanged;
}
