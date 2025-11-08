using UnityEngine;

public class DrivingWheelUpgrade : WheelUpgrade  // generic for other wheels
{
    [SerializeField] private DrivingWheel _drivingWheel;

    public DrivingWheel DrivingWheel => _drivingWheel;
}
