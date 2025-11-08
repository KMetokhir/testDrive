
public abstract class RotaryWheel : DrivingWheel, IRotaryWheel
{
    private WheelRotator _rotator;

    public void RotateWheel(float angle, Rotation rotation)
    {
        _rotator.Rotate(LookDirection, angle, rotation);
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