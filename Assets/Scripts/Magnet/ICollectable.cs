public interface ICollectable : IPoollable
{
    public void Collect();
    public uint Weight { get; }
    public uint Cost { get; }
}