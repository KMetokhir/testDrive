using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Car : MonoBehaviour, ISeller, ICarBody, ICarDirection //, ILevel
{
    [SerializeField] private Magnet _magnet;
    [SerializeField] private Trunk _trunk;
    [SerializeField] private Money _money;

    [SerializeField] private FixedJoint _joint;

    [SerializeField] private CraneSpawner _craneSpawner;

    private Rigidbody _rigidbody;

    /* private ILevel _level;*/
   // public uint Level { get; private set; }

    public Rigidbody Rigidbody => _rigidbody;

    public Vector3 ForwardDirection => transform.forward;

    public Vector3 DownDirection => -transform.up;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _craneSpawner.TypedPartSpawned += OnCraneSpawned;
    }

    private void OnCraneSpawned(Crane crane)
    {
        _joint.connectedBody = crane.Rigidbody; // tmp

        SubscribeToCrane(crane);
    }

    private void SubscribeToCrane(Crane crane) // same in carDriver 
    {
        crane.Destroied += OnCraineDestroied;
        crane.MagnetSpawned += OnMagnetSpawned;
        crane.MagnetDestroied += OnMagnetDestroied;

    }

    private void UnsubscribeFromCrane(Crane crane)
    {
        crane.Destroied -= OnCraineDestroied;
        crane.MagnetSpawned -= OnMagnetSpawned;
        crane.MagnetDestroied -= OnMagnetDestroied;
    }

    private void OnMagnetDestroied(Magnet magnet)
    {
        if (_magnet == magnet)
        {
            _magnet.ObjectInMagnetAria -= OnObjectInMagnetAria;
            _magnet.Stop();
            _magnet = null;
        }
    }

    private void OnCraineDestroied(ObservableUpgradePart part)
    {
        Crane crane = part as Crane;

        if (crane == null)
        {
            throw new Exception("part not Crane type");
        }

        _magnet.Stop();
        _magnet.ObjectInMagnetAria -= OnObjectInMagnetAria;
        _magnet = null;

        UnsubscribeFromCrane(crane);
    }

    private void OnMagnetSpawned(Magnet magnet)
    {
        _magnet = magnet;
        _magnet.ObjectInMagnetAria += OnObjectInMagnetAria;
        _magnet.StartWorke();
    }

    private void Start()
    {
        if (_magnet != null)
        {
            _magnet.StartWorke();
        }

      //  Level = 1; // tmp
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
