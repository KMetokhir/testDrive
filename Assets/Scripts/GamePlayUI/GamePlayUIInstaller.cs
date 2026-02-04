using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GamePlayUIInstaller : MonoInstaller
{

    [SerializeField] private GamePlayUI _gamePlayUIPrefab;

    private GamePlayUI _gamePlayUIInstance;

    private TrunkView _trunkView;
    private MoneyView _moneyView; 
    private ScreenInput _screenInput;
    private UpgradePanel _upgradePanel;
    private LevelUpVeiw _levelUpView;
    private SpeedUpgraderView _speedUpgraderView;
    private MagnetUpgraderView _magnetUpgrader;
    private PowerUpgraderView _powerUpgraderView;

    public override void InstallBindings()
    {

        if (_gamePlayUIInstance == null)
        {
            _gamePlayUIInstance = Instantiate(_gamePlayUIPrefab);

            _trunkView = _gamePlayUIInstance.GetComponentInChildren<TrunkView>();
            _moneyView = _gamePlayUIInstance.GetComponentInChildren<MoneyView>();
            _screenInput = _gamePlayUIInstance.GetComponentInChildren<ScreenInput>();
            _upgradePanel = _gamePlayUIInstance.GetComponentInChildren<UpgradePanel>();
            _levelUpView = _gamePlayUIInstance.GetComponentInChildren<LevelUpVeiw>();
            _speedUpgraderView = _gamePlayUIInstance.GetComponentInChildren<SpeedUpgraderView>();
            _magnetUpgrader = _gamePlayUIInstance.GetComponentInChildren<MagnetUpgraderView>();
            _powerUpgraderView = _gamePlayUIInstance.GetComponentInChildren<PowerUpgraderView>();
        }

        Container.Bind<TrunkView>()
            .FromInstance(_trunkView)
            .AsSingle()
            .NonLazy();

        Container.Bind<MoneyView>()
             .FromInstance(_moneyView)
            .AsSingle()
            .NonLazy();

        Container.Bind<ScreenInput>()
             .FromInstance(_screenInput)
            .AsSingle()
            .NonLazy();

        Container.Bind<UpgradePanel>()
            .FromInstance(_upgradePanel)
           .AsSingle()
           .NonLazy();

        Container.Bind<LevelUpVeiw>()
           .FromInstance(_levelUpView)
           .AsSingle()
           .NonLazy();

        Container.Bind<SpeedUpgraderView>()
            .FromInstance(_speedUpgraderView)
           .AsSingle()
           .NonLazy();

        Container.Bind<MagnetUpgraderView>()
           .FromInstance(_magnetUpgrader)
          .AsSingle()
          .NonLazy();

        Container.Bind<PowerUpgraderView>()
           .FromInstance(_powerUpgraderView)
          .AsSingle()
          .NonLazy();
    }
}
