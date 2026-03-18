using Zenject;

public class MagnetSetingsProvider : CarComponentProvider<IMagnetData>, IMagnetData
{
    public float MagnetRadius => Component?.MagnetRadius ?? 0;

    public MagnetSetingsProvider(SignalBus signalBus) : base(signalBus) { }
}