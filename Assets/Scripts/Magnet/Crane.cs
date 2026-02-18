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
