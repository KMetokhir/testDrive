using System;
using System.Collections.Generic;
using UnityEngine;

public class Crane : CompositePart
{
    [SerializeField] private MagnetSpawner _spawner;
    [SerializeField] private Rope _rope;
    [SerializeField] private CraneArrow _arrow;

    [SerializeField] private Magnet _magnet; // magnetUpgrade not magnet

    private FixedJoint _fixedJoint;

    public event Action<Magnet> MagnetSpawned;
    public event Action<Magnet> MagnetDestroied;

    private void Awake()
    {
        _fixedJoint = GetComponentInChildren<FixedJoint>();
       _arrow = GetComponentInChildren<CraneArrow>();       
    }

    private void OnEnable()
    {
        _spawner.PartSpawned += OnMagnetSpawned;
    }

    private void Start()
    {
        _spawner = GetComponentInChildren<MagnetSpawner>();
        _arrow.ConnectTarget(_rope.GetComponent<Rigidbody>());
    }

    private void OnDisable()
    {
        if (_spawner != null) // tmp
        {
            _spawner.PartSpawned -= OnMagnetSpawned;
        }
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

    private void OnMagnetSpawned(Magnet magnet)
    {
        Debug.Log("MagnetSpawned");

        

       _rope.ConnectTarget(magnet.GetComponent<Rigidbody>(), magnet);

        MagnetSpawned?.Invoke(magnet);
        _magnet = magnet;
        _magnet.Destroied += OnMagnetDestoied;
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