using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Car : MonoBehaviour, ISeller, ICarLevel, ICarBody, ICarDirection //, ILevel
{
    [SerializeField] private Magnet _magnet;
    [SerializeField] private Trunk _trunk;
    [SerializeField] private Money _money;

    [SerializeField] private FixedJoint _joint;

    [SerializeField] private CraneSpawner _craneSpawner;

    private Rigidbody _rigidbody;

    /* private ILevel _level;*/
    public uint Level { get; private set; }

    public Rigidbody Rigidbody => _rigidbody;

    public Vector3 ForwardDirection => transform.forward;

    public Vector3 DownDirection => -transform.up;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _craneSpawner.PartSpawned += OnCraneSpawned;
    }

    private void OnCraneSpawned(Crane crane)
    {
        _joint.connectedBody = crane.Rigidbody; // tmp
        crane.Destroied += OnCraineDestoied;
        crane.MagnetSpawned += OnMagnetSpawned;
    }

    private void OnCraineDestoied(ObservableUpgradePart part)
    {
        throw new NotImplementedException();
    }

    private void OnMagnetSpawned(Magnet magnet)
    {
        magnet.Destroied += OnMagnrtDestroied;
        throw new NotImplementedException();
    }

    private void OnMagnrtDestroied(ObservableUpgradePart part)
    {
        throw new NotImplementedException();
    }

    private void Start()
    {
        if (_magnet != null)
        {
            _magnet.StartWorke();
        }

        Level = 1;
    }

    private void OnEnable()
    {
        if (_magnet != null) // tmp
        {
            _magnet.ObjectInMagnetAria += OnObjectInMagnetAria;
        }

        _trunk.MaxWeightChanged += OnMaxWeightChanged;
    }

    private void OnDisable()
    {
        _magnet.ObjectInMagnetAria -= OnObjectInMagnetAria;
        _trunk.MaxWeightChanged -= OnMaxWeightChanged;
    }

    private void OnMaxWeightChanged()
    {
        if (_magnet != null) // tmp
        {
            _magnet.Stop();
            _magnet.StartWorke();
        }
    }

    private void OnObjectInMagnetAria(ICollectable collectable)
    {
        if (_trunk.TryAdd(collectable) == false)
        {
            _magnet.Stop();
        }
    }

    public List<IAttractable> Buy()
    {
        uint money = _trunk.GetSum();
        _money.Increase(money);

        return _magnet.GetAttractedObjects();
    }
}
