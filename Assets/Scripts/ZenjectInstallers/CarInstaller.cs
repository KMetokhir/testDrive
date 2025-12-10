using System.Collections;
using System.Collections.Generic;
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

        var obj = _carFactory.Create();
    }
}
