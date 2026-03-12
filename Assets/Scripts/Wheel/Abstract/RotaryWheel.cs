public abstract class RotaryWheel : DrivingWheel, IRotaryWheel
{
    private WheelRotator _rotator;

    public void RotateWheel(float angle)
    {
        _rotator.Rotate(LookDirection, angle);
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