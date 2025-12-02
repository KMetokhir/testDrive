using System;
using System.Collections.Generic;
using UnityEngine;

public class Crane : CompositePart
{
    [SerializeField] private MagnetSpawner _spawner;
    [SerializeField] private Magnet _magnet;
    [SerializeField] private Rope _rope;
    [SerializeField] private Rigidbody _rigidbody;
   // [SerializeField] private FixedJoint _joint;

    private Magnet _magnetUpgrade; // magnetUpgrade not magnet

    public Rigidbody Rigidbody => _rigidbody;

    public event Action<Magnet> MagnetSpawned;
    public event Action<Magnet> MagnetDestroied;

    private void Start()
    {
        _spawner = GetComponent<MagnetSpawner>();

      //  _rope.ConnectTarget(_magnet.GetComponent<Rigidbody>());

    //  Car  carBody = FindAnyObjectByType<Car>();//tmp
     //   _joint.connectedBody = carBody.Rigidbody;
    }       

    private void OnEnable()
    {
        _spawner.PartSpawned += OnMagnetSpawned;
    }

    private void OnDisable()
    {
        _spawner.PartSpawned -= OnMagnetSpawned;
    }

    private void OnMagnetSpawned(Magnet magnet)
    {
        _rope.ConnectTarget(magnet.GetComponent<Rigidbody>());

        MagnetSpawned?.Invoke(magnet);
        _magnetUpgrade = magnet;
        _magnetUpgrade.Destroied += OnMagnetDestoied;
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
        _magnetUpgrade.Destroied -= OnMagnetDestoied;
        _magnetUpgrade.DestroyObject();

    }

    

    private void OnMagnetDestoied(ObservableUpgradePart part)
    {
        Magnet magnet = part as Magnet;

        if (magnet != null)
        {
           _magnetUpgrade.Destroied-= OnMagnetDestoied;
           _magnetUpgrade = null;
            MagnetDestroied?.Invoke(magnet);
        }
        else
        {
            throw new Exception("Not Magnet type");
        }
    }
}
