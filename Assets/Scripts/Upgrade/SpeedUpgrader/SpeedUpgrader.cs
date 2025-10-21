using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpeedUpgrader : MonoBehaviour
{
    [SerializeField] private List<SpeedUpgrade> _upgrades;
    [SerializeField] private Transform _carBody;
    [SerializeField] private UpgraderView _view;
    [SerializeField] private Money _money;

    private ICarLevel _carLevel;

    private List<UpgradePart> _installedUpgradeParts;

    private SpeedUpgrade _currentUpgrade;

    public event Action<ISpeedUpgradeData> UpgradeExecuted;

    private void Awake()
    {
        _installedUpgradeParts = new List<UpgradePart>();

        _carLevel = FindObjectOfType<Car>();

    }

    private void OnEnable()
    {
        _view.UpgradeButtonClicked += Upgrade;
    }

    private void Start()
    {
        _currentUpgrade = FindUpgrade(1, 0);// tmp
        _installedUpgradeParts = _currentUpgrade.Execute(_carBody);
        UpgradeExecuted?.Invoke(_currentUpgrade);

        //if maxUpgradeLevel != _upgrades.Count throw error
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

        SpeedUpgrade upgrade = FindUpgrade(_carLevel.Level, nextLevelUpgrade    );
        bool isExists = upgrade != null;

        if (isExists)
        {
            if (_money.TryDecrease(upgrade.Cost))
            {
                if (_installedUpgradeParts.Count != 0)
                {
                    foreach (UpgradePart upgradePart in _installedUpgradeParts)// find equal upgrades from new and installed
                    {
                        GameObject.Destroy(upgradePart.gameObject);
                    }

                    _installedUpgradeParts.Clear();
                }

                _currentUpgrade = upgrade;
                _installedUpgradeParts = _currentUpgrade.Execute(_carBody);
                UpgradeExecuted?.Invoke(_currentUpgrade);

                _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
            }
        }
    }

    private uint GetMaxUpgradeLevel()
    {
        return _upgrades.Max(upgrade => upgrade.UpgradeLevel);
    }

    private SpeedUpgrade FindUpgrade(uint carLevel, uint upgradeLevel)
    {
        return _upgrades.FirstOrDefault(part => part.CarLevel == carLevel && part.UpgradeLevel == upgradeLevel);
    }
}