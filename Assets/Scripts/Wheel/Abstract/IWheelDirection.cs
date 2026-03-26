using UnityEngine;

public interface IWheelDirection
{
    public Vector3 LookDirectionLocal { get; }
    public Vector3 LookDirectionWorld { get; }
}
