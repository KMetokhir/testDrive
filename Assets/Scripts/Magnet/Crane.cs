using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class Crane : CompositePart
{
    [SerializeField] private MagnetSpawner _spawner;
    [SerializeField] private Rope _rope;
   // [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private Magnet _magnet; // magnetUpgrade not magnet

    private FixedJoint _fixedJoint;

  //  public Rigidbody Rigidbody => _rigidbody;

    public event Action<Magnet> MagnetSpawned;
    public event Action<Magnet> MagnetDestroied;

    private void Awake()
    {
        _fixedJoint = GetComponentInChildren<FixedJoint>();
    }

    private void Start()
    {
       
        _spawner = GetComponentInChildren<MagnetSpawner>();

        /*  MessageBroker.Default
          .Receive<CarSpawned>()        
          .Subscribe(msg => OnCarSpawned(msg.CarRigidbody))
          .AddTo(this);*/

       /* MessageBroker.Default
           .Receive<CarStartSpawn>()
           .Subscribe(msg =>
           OnCarStartSpawn(msg.CarRigidbody))
           .AddTo(this);

        MessageBroker.Default
          .Receive<CarEndSpawn>()
          .Subscribe(msg =>
          OnCarSpawned(msg.CarRigidbody))
          .AddTo(this);*/

    }

    public void ConnectToBody(Rigidbody rigidbody)
    {
        if (_fixedJoint == null)
        {
            Debug.LogError("JOINT == null");

        }
        else
        {
            _fixedJoint.connectedBody = rigidbody;
        }
    }

   /* private void OnCarSpawned(Rigidbody carRigidbody)
    {
        *//* _magnet.transform.parent = null;
         transform.parent = null;
         _rope.ConnectTarget(_magnet.GetComponent<Rigidbody>());*/

       /* Rigidbody rb = _magnet.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

       Rigidbody ropeRb = rb.GetComponent<Rigidbody>();
        ropeRb.isKinematic = false;
        ropeRb.velocity = Vector3.zero;
        ropeRb.angularVelocity = Vector3.zero;*//*
    }


    private void OnCarStartSpawn(Rigidbody carRigidbody)
    {
        *//* _magnet.transform.parent = carRigidbody.transform; // Maybe hav to create new joint if needded
         _rope.ConnectTarget(null);
         transform.parent = carRigidbody.transform;*//*

        // _rope.Disconnect();

       *//* Rigidbody rb = _magnet.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Rigidbody ropeRb = rb.GetComponent<Rigidbody>();
      ropeRb.isKinematic = true;*//*
    }*/



    private void OnEnable()
    {
        _spawner.PartSpawned += OnMagnetSpawned;
    }

    private void OnDisable()
    {
        if (_spawner != null) // tmp
        {
            _spawner.PartSpawned -= OnMagnetSpawned;
        }
    }

    private void OnMagnetSpawned(Magnet magnet)
    {

        // _spawner.SetParent(transform.parent);

        //  magnet.transform.parent = transform.parent;
        Debug.Log("MagnetSpawned");

        _rope.ConnectTarget(magnet.GetComponent<Rigidbody>());

        MagnetSpawned?.Invoke(magnet);
        _magnet = magnet;
        _magnet.Destroied += OnMagnetDestoied;
    }

    public override List<UpgradePartSpawner> GetSpawners()
    {
        List<UpgradePartSpawner> spawners = new List<UpgradePartSpawner>();

        if (_spawner != null)
        {
            spawners.Add(_spawner);
        }

        return spawners;
    }

    protected override void DestroyDependentParts()
    {
        _magnet.Destroied -= OnMagnetDestoied;
        _magnet.DestroyObject();

    }


    private void OnMagnetDestoied(ObservableUpgradePart part)
    {
        Magnet magnet = part as Magnet;

        if (magnet != null)
        {
            _magnet.Destroied -= OnMagnetDestoied;
            _magnet = null;
            MagnetDestroied?.Invoke(magnet);
        }
        else
        {
            throw new Exception("Not Magnet type");
        }
    }
}
