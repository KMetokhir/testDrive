using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class LevelUpSystem : MonoBehaviour, ILevel
{
    [SerializeField] private uint _currentLevel;
    [SerializeField] private uint _upgradesToLevelUp;

    [SerializeField] private LevelUpVeiw _view;

    private List<IUpgradable> _upgradables;

    [Inject]
    private void Construct(LevelUpVeiw view)
    {
        _view = view;
    }

    private uint _currentUpgradesLevel => (uint)_upgradables.Sum(v => v.UpgradeLevel);

    public uint Value => _currentLevel;

    private void Awake()
    {
        _upgradables = GetComponents<IUpgradable>().ToList();

        _view.ShowValue(_currentUpgradesLevel, _upgradesToLevelUp);

        if (_currentUpgradesLevel >= _upgradesToLevelUp)
        {
            _view.ActivateButton();
        }
        else
        {
            _view.DeactivateButton();           
        }
    }

    private void OnEnable()
    {
        foreach (IUpgradable upgradable in _upgradables)
        {
            upgradable.Upgraded += OnUpgraded;
        }

        _view.ButtonClicked += OnLevelUpButtonClicked;
    }

    private void OnLevelUpButtonClicked()
    {
        Debug.Log("TO NEW LEVEL");
    }

    private void OnUpgraded()
    {
        _view.ShowValue(_currentUpgradesLevel, _upgradesToLevelUp);

        if (_currentUpgradesLevel >= _upgradesToLevelUp)
        {
            _view.ActivateButton();
        }
    }
}
