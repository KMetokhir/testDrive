using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "CarUpgrades/Speed")]
public class SpeedUpgrade : ScriptableObject
{
    [SerializeField] private uint _carLevel;
    [SerializeField] private uint _speedLevel;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private List<UpgradePart> _upgradeParts;

    public uint CarLevel;
    public uint SpeedLevel;
    public float Acceleration;
    public float MaxSpeed;

    public List<UpgradePart> GetUpgradeParts()
    {
        if (_upgradeParts == null || _upgradeParts.Count == 0)
        {
            throw new NullReferenceException(nameof(_upgradeParts));
        }

        List<UpgradePart> upgradeParts = _upgradeParts.Select(part => part.Clone()).ToList();

        return upgradeParts;
    }
}
