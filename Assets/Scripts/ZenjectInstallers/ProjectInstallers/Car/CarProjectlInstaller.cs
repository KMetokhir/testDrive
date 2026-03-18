using UnityEngine;
using Zenject;

public class CarProjectlInstaller : MonoInstaller
{
    [SerializeField] private CarConteiner _carPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<CarSpawnedSignal>();

        Container.Bind<CarConteiner>().FromInstance(_carPrefab)
            .WhenInjectedInto<RuntimeCarFactory>();

        Container.BindInterfacesAndSelfTo<CarLevelProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<TrunkSettingsProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<MagnetSetingsProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<DriveDataProvader>().AsSingle();
        Container.BindInterfacesAndSelfTo<CarBodyProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<CarDataSaver>().AsSingle();       

        Container.Bind<RuntimeCarFactory>().AsSingle().NonLazy();
    }
}