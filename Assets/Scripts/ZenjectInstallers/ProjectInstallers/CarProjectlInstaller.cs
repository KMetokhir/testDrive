using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class CarProjectlInstaller : MonoInstaller
{
    [SerializeField] private CarConteiner _carPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<CarSpawnedSignal>();

        Container.Bind<ICarLevel>().FromComponentInParents();//.AsSingle();      

        Container.Bind<CarConteiner>().FromInstance(_carPrefab)
            .WhenInjectedInto<RuntimeCarFactory>();

        

        MagnetSetingsProvider magnetSettingsProvider = new MagnetSetingsProvider();
        Container.Bind<MagnetSetingsProvider>().FromInstance(magnetSettingsProvider).AsSingle().WhenInjectedInto<RuntimeCarFactory>();
        Container.Bind<IMagnetUpgradeData>().FromInstance(magnetSettingsProvider).AsSingle();

        Container.Bind<RuntimeCarFactory>().AsSingle().NonLazy();
    }  
}
