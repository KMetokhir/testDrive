using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class CarConteiner : MonoBehaviour
{
    private CarDestroyer _carDestroier;

    private void Awake()
    {
        if (TryGetComponent<CarDestroyer>(out _carDestroier) == false)
        {
            throw new NullReferenceException($"{nameof(_carDestroier)} = null");
        }
    }

    public void Destroy()
    {
        _carDestroier.Destroy();
    }
}
