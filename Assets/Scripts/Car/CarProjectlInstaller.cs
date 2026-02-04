using UnityEngine;
using Zenject;

public class CarProjectlInstaller : MonoInstaller
{
    [SerializeField] private CarConteiner _carPrefab;

    public override void InstallBindings()
    {

        Container.Bind<ILevel>().FromComponentInParents().AsSingle();

        Container.BindFactory<CarConteiner, CarConteiner.Factory>().FromComponentInNewPrefab(_carPrefab);

    }
}
