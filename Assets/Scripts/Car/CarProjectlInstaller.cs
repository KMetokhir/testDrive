using UnityEngine;
using Zenject;

public class CarProjectlInstaller : MonoInstaller
{
    [SerializeField] private CarConteiner _carPrefab;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<CarSpawnedSignal>();


        Container.Bind<ILevel>().FromComponentInParents().AsSingle();


        Container.Bind<CarConteiner>().FromInstance(_carPrefab)
            .WhenInjectedInto<RuntimeCarFactory>();

        Container.Bind<RuntimeCarFactory>().AsSingle().NonLazy();
    }
}
