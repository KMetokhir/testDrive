using Zenject;

public class DriveDataProvader : CarComponentProvider<IDriveData>, IDriveData
{
    public uint UpgradeLevel => Component.UpgradeLevel;

    public uint Acceleration => Component.Acceleration;

    public uint MaxSpeed => Component.MaxSpeed;

    public float RotationSpeed => Component.RotationSpeed;

    public float MaxAngle => Component.MaxAngle;

    public float AckermannMultiplier => Component.AckermannMultiplier;

    public uint StopSpeed => Component.StopSpeed;

    public uint ChangeDirectionSpeed => Component.ChangeDirectionSpeed;

    public DriveDataProvader(SignalBus signalBus) : base(signalBus) { }
}