using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class LevelUpSystem : MonoBehaviour, ICarLevel
{
    [SerializeField] private uint _currentLevel;
    [SerializeField] private uint _upgradesToLevelUp;

    [SerializeField] private LevelUpVeiw _view;
    [SerializeField] private SceneLoadHandler _sceneLoadHandler;

    [SerializeField] private List<IUpgradeable> _upgradables;

    public event Action Changed;

    private uint _currentUpgradesLevel => (uint)_upgradables.Sum(v => v.UpgradeLevel);

    public uint Value => _currentLevel;

    [Inject]
    private void Construct(LevelUpVeiw view, SceneLoadHandler sceneLoadHandler)
    {
        _view = view;
        _sceneLoadHandler = sceneLoadHandler;
    }

    private void Awake()
    {
        _upgradables = GetComponentsInChildren<IUpgradeable>().ToList();

        Debug.LogError(_upgradables.Count);

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
        foreach (IUpgradeable upgradable in _upgradables)
        {
            upgradable.Upgraded += OnUpgraded;
        }

        _view.ButtonClicked += OnLevelUpButtonClicked;
        _sceneLoadHandler.SceneLoaded += OnSceneLoaded;
    }

    private void OnUpgraded()
    {
        _view.ShowValue(_currentUpgradesLevel, _upgradesToLevelUp);

        if (_currentUpgradesLevel >= _upgradesToLevelUp)
        {
            _view.ActivateButton();
        }
    }

    private void Start()
    {
        Changed?.Invoke();
    }

    private void OnDisable()
    {
        foreach (IUpgradeable upgradable in _upgradables)
        {
            upgradable.Upgraded -= OnUpgraded;
        }

        _view.ButtonClicked -= OnLevelUpButtonClicked;
        _sceneLoadHandler.SceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded()
    {
        Changed?.Invoke();
    }

    private void OnLevelUpButtonClicked()
    {
        Debug.Log("TO NEW LEVEL");
    }
}