using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeedUpgradeData
{
    public uint UpgradeLevel { get; }
    public uint Acceleration { get; }
    public uint MaxSpeed { get; }
}
