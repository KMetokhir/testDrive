using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField] private TrunkView _trunkView;
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private CarConteiner _Prefab;
    [SerializeField] private ScreenInput _screenInput;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private LevelUpVeiw _levelUpView;
    [SerializeField] private SpeedUpgraderView _speedUpgraderView;
    [SerializeField] private MagnetUpgraderView _magnetUpgrader;
    [SerializeField] private PowerUpgraderView _powerUpgraderView;

    public override void InstallBindings()
    {
        Container.BindInstance(_trunkView).AsSingle();
        Container.BindInstance(_moneyView).AsSingle();
        Container.BindInstance(_screenInput).AsSingle();
        Container.BindInstance(_levelUpView).AsSingle();
        Container.BindInstance(_speedUpgraderView).AsSingle();
        Container.BindInstance(_magnetUpgrader).AsSingle();
        Container.BindInstance(_powerUpgraderView).AsSingle();
        Container.BindInstance(_upgradePanel).AsSingle();


        Container.BindFactory<CarConteiner, CarConteiner.Factory>().FromComponentInNewPrefab(_Prefab);


        //   Container.BindFactory<Money, Money.Factory>().FromComponentInNewPrefab(_Prefab);




    }
}
