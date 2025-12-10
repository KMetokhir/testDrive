using UnityEngine;
using Zenject;

public class CarConteiner : MonoBehaviour
{

    public class Factory : PlaceholderFactory<CarConteiner>
    {
    }
}
