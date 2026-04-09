using Zenject;

public class LeftRotaryWheel : RotaryWheel<LeftWheelRotator>
{
    [Inject]
    private LeftWheelRotator.Factory _factory;

    protected override LeftWheelRotator CreateRotator()
    {
        return _factory.Create(this);
    }
}