using Zenject;

public class MagnetSetingsProvider : CarComponentProvider<IMagnetData>, IMagnetData
{
    public float MagnetRadius => Component.MagnetRadius;

    public MagnetSetingsProvider(SignalBus signalBus) : base(signalBus) { }
}