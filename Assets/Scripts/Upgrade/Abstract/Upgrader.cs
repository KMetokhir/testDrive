using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Upgrader<T, S, M> : MonoBehaviour
    where S : ButtonWithSliderView
    where M : IUpgradeData
    where T : Upgrade, M
{
    [SerializeField] private List<T> _upgrades;

    [SerializeField] private S _view;
    [SerializeField] private Money _money;

    [SerializeField] private Transform _carBody;
    [SerializeField] private Transform _crane;


    private ICarLevel _carLevel;

    private List<UpgradePart> _installedUpgradeParts;

    private T _currentUpgrade;

    public event Action<M> UpgradeExecuted;

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

        _installedUpgradeParts = _currentUpgrade.Execute();
        foreach (UpgradePart part in _installedUpgradeParts)
        {
            Transform parent = DetermineParent(part);
            part.transform.position = parent.TransformPoint(part.SpawnPosition);
            part.transform.rotation = parent.transform.rotation;
            part.transform.parent = parent;
        }

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
                    foreach (UpgradePart upgradePart in _installedUpgradeParts)// find equal upgrades from new and installed
                    {
                        GameObject.Destroy(upgradePart.gameObject);
                    }

                    _installedUpgradeParts.Clear();
                }

                _currentUpgrade = upgrade;
                _installedUpgradeParts = _currentUpgrade.Execute();

                foreach (UpgradePart part in _installedUpgradeParts)
                {
                    Transform parent = DetermineParent(part);
                    part.transform.position = parent.TransformPoint(part.SpawnPosition);
                    part.transform.rotation = parent.transform.rotation;
                    part.transform.parent = parent;
                }

                UpgradeExecuted?.Invoke((M)_currentUpgrade);

                _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
            }
        }
    }

    private Transform DetermineParent(UpgradePart part)
    {
        return part switch
        {
            BodyPart => _carBody,
            CranePArt => _crane,
            _ => throw new Exception("Undefined parent ")
        };
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
