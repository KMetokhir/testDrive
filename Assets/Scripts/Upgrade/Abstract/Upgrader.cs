using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class Upgrader<T, S, M> : MonoBehaviour, IUpgrader<M>
    where S : ButtonWithSliderView
    where M : IUpgradeData
    where T : Upgrade, M
{
    [SerializeField] private List<T> _upgrades;
    [SerializeField] private List<UpgradePartSpawner> _spawners;

    [SerializeField] private Money _money;

    [SerializeField] private List<UpgradePartSpawner> _compositePartSpawners;  // Observable part spawner
                                                                               // [SerializeField] private List<ObservableUpgradePart> _observableParts;

    [SerializeField] private List<UpgradePart> _installedUpgradeParts;

    [SerializeField] private S _view;

    private ICarLevel _carLevel;
    private ICarConfigSaver _configSaver;

    private uint _currentUpgradeLevel;

    private DiContainer _container;

    private T _currentUpgrade;

    public event Action<M> UpgradeExecuted;

    [Inject]
    private void Construct(S view, ICarLevel carLevel, DiContainer container, ICarConfigSaver configSaver)
    {
        _container = container;
        _view = view;
        _carLevel = carLevel;
        _configSaver = configSaver;
    }

    private void OnEnable()
    {
        _view.UpgradeButtonClicked += Upgrade;
    }

    private void Start()
    {
        _compositePartSpawners = new List<UpgradePartSpawner>();
        _installedUpgradeParts = new List<UpgradePart>();

        _currentUpgradeLevel = _configSaver.GetCarConfig(GetType().Name, _carLevel.Value);

        LoadUpgradesInOrder(_currentUpgradeLevel);

        if (_currentUpgrade != null)
        {
            _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());
        }
        else
        {
            throw new Exception($"No avalible upgarde for carLevel {_carLevel.Value}");
        }
    }

    private void OnDisable()
    {
        _view.UpgradeButtonClicked -= Upgrade;
    }

    private void LoadUpgradesInOrder(uint currentUpgradeLevel)
    {
        List<T> upgrades = _upgrades;

        upgrades = upgrades.OrderBy(upgrade => upgrade.UpgradeLevel).Where(upgrade => upgrade.UpgradeLevel <= currentUpgradeLevel).ToList();

        foreach (T upgrade in upgrades)
        {
            ProcessUpgrade(upgrade);
        }

        _currentUpgrade = FindUpgrade(_carLevel.Value, _currentUpgradeLevel);// tmp

        if (_currentUpgrade != null)
        {
            UpgradeExecuted?.Invoke((M)_currentUpgrade);
        }
        else
        {
            throw new Exception($"No avalible upgarde for carLevel {_carLevel.Value}");
        }
    }

    private void Upgrade()
    {
        if (_upgrades == null || _upgrades.Count == 0)
        {
            Debug.Log((_upgrades.Count == 0) + " " + _upgrades == null);
            throw new NullReferenceException(nameof(_installedUpgradeParts));
        }

        uint nextLevelUpgrade = _currentUpgrade.UpgradeLevel + 1;

        T upgrade = FindUpgrade(_carLevel.Value, nextLevelUpgrade);
        bool isExists = upgrade != null;

        if (isExists)
        {
            if (_money.TryDecrease(upgrade.Cost))
            {
                _currentUpgrade = upgrade;

                ProcessUpgrade(_currentUpgrade);

                UpgradeExecuted?.Invoke((M)_currentUpgrade);

                _view.ShowValue(_currentUpgrade.UpgradeLevel, GetMaxUpgradeLevel());

                _currentUpgradeLevel = _currentUpgrade.UpgradeLevel;

                _configSaver.SaveCarConfig(GetType().Name, _carLevel.Value, _currentUpgradeLevel);
            }
        }
    }

    private void SpawnParts(List<UpgradePartSpawner> spawners, List<UpgradePart> newParts, List<UpgradePart> installedParts)
    {
        for (int i = newParts.Count - 1; i >= 0; i--)
        {
            foreach (UpgradePartSpawner spawner in spawners)
            {
                if (spawner.IsSpawnPossible(newParts[i]))
                {
                    var oldpart = spawner.LastSpawnedPart;

                    if (oldpart != null)
                    {
                        _installedUpgradeParts.Remove(oldpart);
                        oldpart.DestroyObject();
                    }

                    if (spawner.TrySpawn(newParts[i]))
                    {
                        ProcessCompositPart(newParts[i]);
                        ProcessObservablePart(newParts[i]);

                        installedParts.Add(newParts[i]);
                        newParts.RemoveAt(i);

                        break;
                    }
                    else
                    {
                        throw new Exception("Unknown exception, spawn possible, but false");
                    }
                }
            }
        }
    }

    private void ProcessUpgrade(T upgrade)
    {
        List<UpgradePart> newParts = upgrade.Execute(_container);
        List<UpgradePart> installedParts = new List<UpgradePart>();

        SpawnParts(_spawners, newParts, installedParts);

        SpawnParts(_compositePartSpawners, newParts, installedParts);

        _installedUpgradeParts.AddRange(installedParts);

        if (newParts.Count != 0)
        {
            foreach (var part in newParts)
            {
                Debug.Log(part.ToString() + " dont installed");
            }

            throw new Exception("Some upgrade parts not Spawned ");
        }
    }

    private void ProcessObservablePart(UpgradePart part)
    {
        ObservableUpgradePart observablePart = part as ObservableUpgradePart;

        if (observablePart != null)
        {
            observablePart.Destroied += RemoveObservablePart;
        }
    }

    private void RemoveObservablePart(ObservableUpgradePart part)
    {
        _installedUpgradeParts.Remove(part);
    }

    private void ProcessCompositPart(UpgradePart part)
    {
        CompositePart compositePart = part as CompositePart;

        if (compositePart != null)
        {
            _compositePartSpawners.AddRange(compositePart.GetSpawners());
            compositePart.Destroied += OnCompositePartDestroied;
        }
    }

    private void OnCompositePartDestroied(ObservableUpgradePart part)
    {
        part.Destroied -= OnCompositePartDestroied;

        CompositePart compositePart = part as CompositePart;

        if (compositePart != null)
        {
            _compositePartSpawners = _compositePartSpawners.Except(compositePart.GetSpawners()).ToList();
        }
        else
        {
            throw new Exception("Incorrect input data " + nameof(part));
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