using System;
using UnityEngine;

public class MovingObject
{
    public readonly ITransformContainer TransformContainer;
    public readonly Vector3 StartPos;

    private readonly ITransformContainer _transformContainer;

    public float Time { get; private set; }

    public MovingObject(ITransformContainer transformContainer, float time)
    {
        _transformContainer = transformContainer;
        StartPos = transformContainer.Transform.position;
        Time = time;
        TransformContainer = transformContainer;
    }

    public void IncreaseTime(float value)
    {
        value = Math.Abs(value);

        Time += value;
    }

    public void SetPosition(Vector3 position)
    {
        TransformContainer.Transform.position = position;
    }
}
