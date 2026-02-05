using UnityEngine;
using Zenject;

public class CarProjectlInstaller : MonoInstaller
{
    [SerializeField] private CarConteiner _carPrefab;

    public override void InstallBindings()
    {

        Container.Bind<ILevel>().FromComponentInParents().AsSingle();

       // Container.BindFactory<CarConteiner, CarConteiner.Factory>().FromComponentInNewPrefab(_carPrefab).AsSingle();
        Container.Bind<CarConteiner>().FromInstance(_carPrefab).AsSingle();
       // Container.Bind<ICarBody>().FromInstance(_carPrefab.GetComponentInChildren<ICarBody>()).AsSingle();

        Container.Bind<RuntimeCarFactory>().AsSingle();

        

       


        /* Container.Bind<ILevel>().FromComponentInParents().AsSingle();

         // Bind the default prefab
         Container.Bind<CarConteiner>().FromInstance(_defaultCarPrefab).AsSingle();

         // Bind a simple runtime factory
         Container.Bind<RuntimeCarFactory>().AsSingle();*/

    }
}
