using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private Magnet _magnet;
    [SerializeField] private Trunk _trunk;

    private void Awake()
    {
        _magnet.StartWorke();
    }

    private void OnEnable()
    {
        _magnet.ObjectInMagnetAria += OnObjectInMagnetAria;
    }
    private void OnDisable()
    {
        _magnet.ObjectInMagnetAria -= OnObjectInMagnetAria;

    }

    private void OnObjectInMagnetAria(ICollectable collectable)
    {
        if (_trunk.TryAdd(collectable) == false)
        {
            _magnet.Stop();
        }        
    }
}
