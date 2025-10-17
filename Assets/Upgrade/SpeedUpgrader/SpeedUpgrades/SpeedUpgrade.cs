using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "CarUpgrades/Speed")]
public class SpeedUpgrade : ScriptableObject, ISpeedUpgradeData
{
    [SerializeField] private uint _cost;
    [SerializeField] private uint _carLevel;
    [SerializeField] private uint _upgradeLevel;
    [SerializeField] private uint _acceleration;
    [SerializeField] private uint _maxSpeed;

    [SerializeField] private List<UpgradePart> _upgradeParts;


    public uint Cost => _cost;
    public uint CarLevel => _carLevel;
    public uint UpgradeLevel => _upgradeLevel;
    public uint Acceleration => _acceleration;
    public uint MaxSpeed => _maxSpeed;

    /* public List<UpgradePart> GetUpgradeParts()
     {
         if (_upgradeParts == null || _upgradeParts.Count == 0)
         {
             throw new NullReferenceException(nameof(_upgradeParts));
         }

         List<UpgradePart> upgradeParts = _upgradeParts.Select(part => part.Clone()).ToList();

         return upgradeParts;
     }*/

    public List<UpgradePart> Execute(Transform parent)
    {
        /*if (_upgradeParts == null || _upgradeParts.Count == 0)
        {
            throw new NullReferenceException(nameof(_upgradeParts));
        }*/

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
