
public interface ISpeedUpgradeData: IUpgradeData
{
    public uint UpgradeLevel { get; }
    public uint Acceleration { get; }
    public uint MaxSpeed { get; }
}
