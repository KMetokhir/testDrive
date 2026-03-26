using System;
using UnityEngine;

public abstract class RotaryWheel<T> : RotaryWheel
    where T : WheelRotator
{
    private T _rotator;

    public override event Action<Vector3> DirectionChanged;

    public override void RotateWheel(float angle)
    {
        _rotator.Rotate(angle);
    }

    public override void StopRotation()
    {
        _rotator.StopRotating();
    }

    protected override void UseInEneble()
    {
        base.UseInEneble();
        _rotator.DirectionChanged += OnDirectionChanged;
    }

    protected override void UseInAwake()
    {
        _rotator = CreateRotator();
        base.UseInAwake();
    }

    protected abstract T CreateRotator();

    protected override void UseInDisable()
    {
        base.UseInDisable();
        _rotator.DirectionChanged -= OnDirectionChanged;
    }

    private void OnDirectionChanged(Vector3 vector)
    {
        DirectionChanged?.Invoke(vector);
    }

    protected override void UseInFixedUpdate()
    {
        _rotator?.FixedUpdate();

        base.UseInFixedUpdate();
    }
}