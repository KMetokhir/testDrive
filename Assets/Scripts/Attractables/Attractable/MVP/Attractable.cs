using System;
using UniRx;
using UnityEngine;

public class Attractable : MonoBehaviour, IAttractable, IAttractableLevel
{
    [SerializeField] private AttractableConfig _config;
    [SerializeField] private  AttractableView _view;

    private AttractableModel _model;

    public bool IsActive => _model.IsActive;
    public bool IsPossibleToCollect => _model.IsPossibleToCollect;
    public Transform Transform => transform;

    public uint Weight => _model.Weight;
    public uint Cost => _model.Cost;
    public uint Level => _model.Level;
    public string Id => _model.Id;
    public AttractablesType Type => _model.Type;

    public event Action<Attractable> Deactivated;
    public event Action<Attractable> Collected;

    private void Awake()
    {
        _model = new AttractableModel(_config);

        if (_view.Type != _model.Type)
        {
            throw new System.Exception($"model.Type {_model.Type} != view.Type {_view.Type}");
        }

        MessageBroker.Default
            .Receive<LevelUp>()
             .Subscribe(msg =>
             _model.ProcessCollectorChangeLevel(msg.Level))
             .AddTo(this);
    }

    private void OnEnable()
    {
        _model.Activated += OnActivated;
        _model.Deactivated += OnDeactivated;
        _model.BecameAvalible += OnBacameAvalible;
        _model.Collected += OnCollected;
    }
    private void OnDisable()
    {
        _model.Activated -= OnActivated;
        _model.Deactivated -= OnDeactivated;
        _model.BecameAvalible -= OnBacameAvalible;
        _model.Collected -= OnCollected;

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


    public void Deactivate()
    {
        _model.Deactivate();
    }
}