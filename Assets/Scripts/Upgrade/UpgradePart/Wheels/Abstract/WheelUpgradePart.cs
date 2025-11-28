
using UnityEngine.UIElements;

public abstract class WheelUpgradePart : ObservableUpgradePart, IWheelUpgrade
{
    public IWheel Wheel { get; private set; }

    private void Awake()
    {
        Wheel = GetComponent<IWheel>();
    }   
}
