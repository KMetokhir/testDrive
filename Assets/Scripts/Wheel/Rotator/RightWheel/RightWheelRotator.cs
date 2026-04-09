using Zenject;

public class RightWheelRotator : WheelRotator
{
    public RightWheelRotator(IWheelDirection wheelDirection, ICarDirection carDirection, IWheelRotationData rotationData) : base(wheelDirection, carDirection, rotationData)
    {
    }

    public override float GetMultiplier(float currentRotationAngle, float ackermannMultiplier)
    {
        return currentRotationAngle > 0 ? ackermannMultiplier : 1;
    }

    public class Factory : PlaceholderFactory<IWheelDirection, RightWheelRotator>
    {
    }
}