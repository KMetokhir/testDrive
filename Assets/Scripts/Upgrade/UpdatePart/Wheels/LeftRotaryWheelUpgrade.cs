
using UnityEngine;

public class LeftRotaryWheelUpgrade : RotaryWheelUpgrade, IWheelUpgrade
{
    [SerializeField] private LeftRotaryWheel _leftRotaryWheel;

    public LeftRotaryWheel LeftRotaryWheel => _leftRotaryWheel;
}
