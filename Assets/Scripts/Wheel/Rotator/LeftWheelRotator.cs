public class LeftWheelRotator : WheelRotator
{
    public override float GetMultiplier(float currentRotationAngle, float ackermannMultiplier)
    {
        return currentRotationAngle < 0 ? ackermannMultiplier : 1;
    }
}