public interface ISpeedData
{
    public uint UpgradeLevel { get; }
    public uint Acceleration { get; }
    public uint MaxSpeed { get; }

    public uint StopSpeed { get; }
    public uint ChangeDirectionSpeed { get; } // Forward Move - decrease speed time - backward move
}