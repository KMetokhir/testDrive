
public abstract class WheelUpgradePart : UpgradePart, IWheelUpgrade
{
    public IWheel Wheel { get; private set; }

    private void Awake()
    {
        Wheel = GetComponent<IWheel>();
    }
}
