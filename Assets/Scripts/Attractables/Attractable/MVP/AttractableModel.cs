using System;
using System.Diagnostics;
using UnityEngine;

public class AttractableModel
{
    public string Id { get; }
    public AttractablesType Type { get; }
    public uint Weight { get; }
    public uint Cost { get; }
    public uint Level { get; }

    private int _collectorLevel;

    public bool IsActive { get; private set; }
    public bool IsAvalibleToCollector { get { return (Level <= _collectorLevel); } }
    public bool IsPossibleToCollect { get { return IsAvalibleToCollector && IsActive; } }

    public event Action Activated;
    public event Action Deactivated;
    public event Action Collected;
    public event Action BecameAvalible;

    public AttractableModel(AttractableConfig config)
    {
        Type = config.Type;
        Weight = config.Weight;
        Cost = config.Cost;
        Level = config.Level;

        if (Weight == 0)
            throw new Exception("Weight = 0");

        if (Cost == 0)
            throw new Exception("Cost = 0");

        Id = GenerateUniqueId();

        int defaultCollectorLevel = -1;
        _collectorLevel = defaultCollectorLevel;

        IsActive = true;
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        Activated?.Invoke();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        Deactivated?.Invoke();
    }

    public void ProcessCollectorChangeLevel(uint collectorNewLevel)
    {        
        bool oldAvalibleStatus = IsAvalibleToCollector;

        _collectorLevel = (int)collectorNewLevel;

        if (oldAvalibleStatus != IsAvalibleToCollector)
        {
            BecameAvalible?.Invoke();
        }
    }

    public void Collect()
    {
        Collected?.Invoke();
    }

    private string GenerateUniqueId()
    {
        string time = DateTime.Now.Ticks.ToString();
        string random = UnityEngine.Random.Range(1000, 9999).ToString();
        return $"{Type}_{time}_{random}";
    }
}