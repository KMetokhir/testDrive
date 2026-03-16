using System;
using UnityEngine;
using Zenject;

public abstract class Attractable : MonoBehaviour, IAttractable, IAttractableLevel
{
    [SerializeField] private AttractableConfig _config;
    [SerializeField] private AttractableView _view;

    private ICarLevel _carLevel;

    private AttractableModel _model;

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

    [Inject]
    private void Construct(ICarLevel carLevel)
    {
        _carLevel = carLevel;
    }

    private void Awake()
    {
        _model = new AttractableModel(_config);
    }

    private void OnEnable()
    {
        _model.Activated += OnActivated;
        _model.Deactivated += OnDeactivated;
        _model.BecameAvalible += OnBacameAvalible;
        _model.Collected += OnCollected;
        _model.Stored += OnStored;

        _carLevel.Changed += OnCarLevelChanged;
    }

    private void OnDisable()
    {
        _model.Activated -= OnActivated;
        _model.Deactivated -= OnDeactivated;
        _model.BecameAvalible -= OnBacameAvalible;
        _model.Collected -= OnCollected;
        _model.Stored -= OnStored;

        _carLevel.Changed -= OnCarLevelChanged;
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

    private void OnCollected()
    {
        Collected?.Invoke(this);

    }

    private void OnActivated()
    {
        _view.Activate();

    }

    private void OnDeactivated()
    {
        _view.Deactivate();
        Deactivated?.Invoke(this);

    }

    private void OnBacameAvalible()
    {
        _view.BecameAvalible();

    }

    public void Collect()
    {
        _model.Collect();

    }

    public void Activate()
    {
        _model.Activate();

    }

    public void Deactivate()
    {
        _model.Deactivate();

    }
}

