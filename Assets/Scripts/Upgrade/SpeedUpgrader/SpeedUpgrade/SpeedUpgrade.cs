using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "CarUpgrades/Speed")]
public class SpeedUpgrade : Upgrade, ISpeedUpgradeData
{
    [SerializeField] private uint _acceleration;
    [SerializeField] private uint _maxSpeed;

    public uint Acceleration => _acceleration;
    public uint MaxSpeed => _maxSpeed;
}
