using System;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class Attractable : MonoBehaviour, IAttractable, IAttractableLevel
{
    [SerializeField] private AttractableConfig _config;
    [SerializeField] private AttractableView _view;
    [SerializeField] private int CARlevel;// tmp
    public bool iscollectable; // TMP

    private ICarLevel _carLevel;

    private AttractableModel _model;
    // public AttractablesType Type => _model.Type;

    public event Action<Attractable> Deactivated;
    public event Action<Attractable> Collected;
    public event Action<Attractable> Stored;

    public bool IsActive => _model.IsActive;
    public bool IsPossibleToCollect => _model.IsPossibleToCollect;
    public Transform Transform => transform;

    public uint Weight => _model.Weight;
    public uint Cost => _model.Cost;
    public uint Level => _model.Level;
    public string Id => _model.Id;

    //private SignalBus _signalBus;

    [Inject]
    private void Construct(/*SignalBus signalBus,*/ ICarLevel carLevel)
    {
        // _signalBus = signalBus;
        _carLevel = carLevel;

        //   Debug.LogError( $" {_carLevel == null} in attractanble");
    }

    private void Awake()
    {
        // CARlevel =(int) _carLevel.Value;

        _model = new AttractableModel(_config);

        /*if (_view.Type != _model.Type)
        {
            throw new System.Exception($"model.Type {_model.Type} != view.Type {_view.Type}");
        }*/

        // Debug.LogError("Subsribe to level up");
        /*
                MessageBroker.Default  // unsubscribe on CompositDisposable in disable?
                    .Receive<LevelUp>()
                     .Subscribe(msg =>
                     _model.ProcessCollectorLevel((int)msg.Level))
                     .AddTo(this);*/

        iscollectable = _model.IsPossibleToCollect;// test
    }

    private void OnEnable()
    {
        _model.Activated += OnActivated;
        _model.Deactivated += OnDeactivated;
        _model.BecameAvalible += OnBacameAvalible;
        _model.Collected += OnCollected;
        _model.Stored += OnStored;

        _carLevel.Changed += OnCarLevelChanged;
        /*  _signalBus.Subscribe<CarSpawnedSignal>(OnCarSpawned);*/
    }

    private void OnDisable()
    {
        _model.Activated -= OnActivated;
        _model.Deactivated -= OnDeactivated;
        _model.BecameAvalible -= OnBacameAvalible;
        _model.Collected -= OnCollected;
        _model.Stored -= OnStored;
        _carLevel.Changed -= OnCarLevelChanged;

        /*  _signalBus.Unsubscribe<CarSpawnedSignal>(OnCarSpawned);*/
    }

    private void OnCarLevelChanged()
    {
        _model.ProcessCollectorLevel((int)_carLevel.Value);
    }

    private void OnStored()
    {
        Stored?.Invoke(this);
    }

    public void TransitToStore()
    {
        _model.TransitToStore();
    }

    /* private void OnCarSpawned(CarSpawnedSignal signal)
     {
         //  Debug.LogError("Car Spawned In Attracrable");

         CARlevel = (int)signal.Car.Value;
         _model.ProcessCollectorLevel(CARlevel);
     }*/

    private void OnCollected()
    {
        Collected?.Invoke(this);
        iscollectable = _model.IsPossibleToCollect;// test
    }

    private void OnActivated()
    {
        _view.Activate();
        iscollectable = _model.IsPossibleToCollect;// test
    }

    private void OnDeactivated()
    {
        _view.Deactivate();
        Deactivated?.Invoke(this);
        iscollectable = _model.IsPossibleToCollect;// test
    }

    private void OnBacameAvalible()
    {
        _view.BecameAvalible();
        iscollectable = _model.IsPossibleToCollect;// test
    }

    public void Collect()
    {
        _model.Collect();
        iscollectable = _model.IsPossibleToCollect;// test
    }

    public void Activate()
    {
        _model.Activate();
        iscollectable = _model.IsPossibleToCollect;// test
    }

    public void Deactivate()
    {
        _model.Deactivate();
        iscollectable = _model.IsPossibleToCollect; // test
    }
}
