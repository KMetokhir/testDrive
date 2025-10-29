using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Upgrader<T, S, M> : MonoBehaviour
    where S : ButtonWithSliderView
    where M : IUpgradeData
    where T : Upgrade, M
{
    [SerializeField] private List<T> _upgrades;
    [SerializeField] private List<UpgradePartsSpawner> _spawners;

    [SerializeField] private S _view;
    [SerializeField] private Money _money;

    private ICarLevel _carLevel;

    private List<UpgradePart> _installedUpgradeParts;

    private T _currentUpgrade;

    public event Action<M> UpgradeExecuted;

    private void Awake()
    {
        _installedUpgradeParts = new List<UpgradePart>();

        _carLevel = FindObjectOfType<Car>();

        _currentUpgrade = FindUpgrade(1, 0);// tmp

        foreach (UpgradePart upgradePart in _installedUpgradeParts)
        {
            GameObject.Destroy(upgradePart.gameObject);
        }

        List<UpgradePart> newParts = _currentUpgrade.Execute();

        foreach (UpgradePart part in newParts)
        {
            foreach (UpgradePartsSpawner spawner in _spawners)
            {
                if (spawner.TrySpawn(part))
                {
                    _installedUpgradeParts.Add(part);
                    break;
                }
            }
        }
    }

    private void OnEnable()
    {
        _view.UpgradeButtonClicked += Upgrade;
    }

    private void Start()
    {
        UpgradeExecuted?.Invoke((M)_currentUpgrade);

        _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
    }

    private void Upgrade()
    {
        if (_upgrades == null || _upgrades.Count == 0)
        {
            Debug.Log((_upgrades.Count == 0) + " " + _upgrades == null);
            throw new NullReferenceException(nameof(_installedUpgradeParts));
        }

        uint nextLevelUpgrade = _currentUpgrade.UpgradeLevel + 1;// tmp

        T upgrade = FindUpgrade(_carLevel.Level, nextLevelUpgrade);
        bool isExists = upgrade != null;

        if (isExists)
        {
            if (_money.TryDecrease(upgrade.Cost))
            {
                if (_installedUpgradeParts.Count != 0)
                {
                    foreach (UpgradePart upgradePart in _installedUpgradeParts)
                    {
                        GameObject.Destroy(upgradePart.gameObject);
                    }

                    _installedUpgradeParts.Clear();
                }

                _currentUpgrade = upgrade;
                List<UpgradePart> newParts = _currentUpgrade.Execute();

                foreach (UpgradePart part in newParts)
                {
                    foreach (UpgradePartsSpawner spawner in _spawners)
                    {
                        if (spawner.TrySpawn(part))
                        {
                            _installedUpgradeParts.Add(part);
                            break;
                        }
                    }
                }

                if (_installedUpgradeParts.Count != newParts.Count)
                {
                    throw new Exception("Unknown upgrade part, need spawner");
                }

                UpgradeExecuted?.Invoke((M)_currentUpgrade);

                _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
            }
        }
    }
    private uint GetMaxUpgradeLevel()
    {
        return _upgrades.Max(upgrade => upgrade.UpgradeLevel);
    }

    private T FindUpgrade(uint carLevel, uint upgradeLevel)
    {
        return _upgrades.FirstOrDefault(part => part.CarLevel == carLevel && part.UpgradeLevel == upgradeLevel);
    }
}