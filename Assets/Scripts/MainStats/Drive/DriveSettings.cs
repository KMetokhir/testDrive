public class DriveSettings : Settings<SpeedUpgrader, IDriveData>, IDriveData
{
    public uint UpgradeLevel { get; private set; }
    public uint Acceleration { get; private set; }
    public uint MaxSpeed { get; private set; }
    public uint StopSpeed { get; private set; }
    public uint ChangeDirectionSpeed { get; private set; }

    public float RotationSpeed { get; private set; }
    public float MaxAngle { get; private set; }
    public float AckermannMultiplier { get; private set; }

    

    protected override void ApplyUpgrade(IDriveData data)
    {
        UpgradeLevel = data.UpgradeLevel;
        Acceleration = data.Acceleration;
        MaxSpeed = data.MaxSpeed;
        StopSpeed = data.StopSpeed;
        ChangeDirectionSpeed = data.ChangeDirectionSpeed;
        RotationSpeed = data.RotationSpeed;
        MaxAngle = data.MaxAngle;
        AckermannMultiplier = data.AckermannMultiplier;
    }
}