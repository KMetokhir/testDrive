
using UnityEngine;

[CreateAssetMenu(fileName = "MagnetUpgrade", menuName = "CarUpgrades/Magnet")]
public class MagnetUpgrade : Upgrade, IMagnetUpgradeData
{
    [SerializeField] private float _magnetRadius;
    public float MagnetRadius => _magnetRadius;
}
