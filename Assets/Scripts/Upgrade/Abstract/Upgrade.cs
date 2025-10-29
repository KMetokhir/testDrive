using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade  : ScriptableObject
{
    [SerializeField] private uint _cost;
    [SerializeField] private uint _carLevel;
    [SerializeField] private uint _upgradeLevel;

    [SerializeField] private List<UpgradePart> _upgradeParts;

    public uint Cost => _cost;
    public uint CarLevel => _carLevel;
    public uint UpgradeLevel => _upgradeLevel;

    public List<UpgradePart> Execute()
    {
        List<UpgradePart> upgradeParts = new List<UpgradePart>();

        foreach (UpgradePart part in _upgradeParts)
        {
            for (int i = 0; i < part.Count; i++)
            {
                UpgradePart item = Instantiate(part);
                upgradeParts.Add(item);
            }
        }

        return upgradeParts;
    }
}
