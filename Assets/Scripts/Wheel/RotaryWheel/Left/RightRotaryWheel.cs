using Zenject;

public class RightRotaryWheel : RotaryWheel<RightWheelRotator>
{
    [Inject]
    private RightWheelRotator.Factory _factory;

    protected override RightWheelRotator CreateRotator()
    {
        return _factory.Create(this);
    }
}