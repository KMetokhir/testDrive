using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpgrade", menuName = "CarUpgrades/Power")]
public class PowerUpgrade : ScriptableObject, IPowerUpgradeData
{
    [SerializeField] private uint _cost;
    [SerializeField] private uint _carLevel;
    [SerializeField] private uint _upgradeLevel;
    [SerializeField] private uint _maxWeight;

    [SerializeField] private List<UpgradePart> _upgradeParts;

    public uint Cost => _cost;
    public uint CarLevel => _carLevel;
    public uint UpgradeLevel => _upgradeLevel;
    public uint MaxWeight  => _maxWeight;

    public List<UpgradePart> Execute(Transform parent)
    {
        List<UpgradePart> upgradeParts = new List<UpgradePart>();

        foreach (UpgradePart part in _upgradeParts)
        {
            UpgradePart item = Instantiate(part);
            item.transform.position = parent.TransformPoint(part.SpawnPosition);
            item.transform.rotation = parent.transform.rotation;
            item.transform.parent = parent;

            upgradeParts.Add(item);
        }

        return upgradeParts;
    }
}
