using UnityEngine;
using Zenject;

public class CarInstaller : MonoBehaviour
{
    private CarConteiner.Factory _carFactory;    

    [Inject]
    public void Construct(CarConteiner.Factory carFactory)
    {
        _carFactory = carFactory;
    }

    private void Start()
    {
        CarConteiner carCont = _carFactory.Create();

       // CarPlacer pl = carCont.GetComponentInChildren<CarPlacer>();

    }   
}
