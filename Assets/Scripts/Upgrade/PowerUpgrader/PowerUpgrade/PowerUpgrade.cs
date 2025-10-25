using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpgrade", menuName = "CarUpgrades/Power")]
public class PowerUpgrade : Upgrade, IPowerUpgradeData
{
    [SerializeField] private uint _maxWeight;

    public uint MaxWeight => _maxWeight;
}
