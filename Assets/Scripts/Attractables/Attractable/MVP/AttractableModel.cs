using System;

public class AttractableModel
{
    private int _collectorLevel;
    private bool _isCollected;

    public event Action Activated;
    public event Action Deactivated;
    public event Action Collected;
    public event Action BecameAvalible;
    public event Action Stored;
    public string Id { get; } 
    public uint Weight { get; }
    public uint Cost { get; }
    public uint Level { get; }

    public bool IsActive { get; private set; }
    public bool IsAvalibleToCollector { get { return (Level <= _collectorLevel); } }
    public bool IsPossibleToCollect { get { return IsAvalibleToCollector && IsActive && _isCollected == false; } }

    public AttractableModel(AttractableConfig config)
    {     
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
        _isCollected = false;
    }

    public void Activate()
    {
        if (IsActive)

            return;

        IsActive = true;
        Activated?.Invoke();

        Reload();
    }

    public void TransitToStore()
    {
        if (_isCollected)
        {
            Stored?.Invoke();
        }
        else
        {
            throw new Exception("cannot be stores befor colect");
        }
    }

    public void Deactivate()
    {
        if (!IsActive)

            return;

        IsActive = false;
        Deactivated?.Invoke();
    }

    public void ProcessCollectorLevel(int collectorLevel)
    {
        if (collectorLevel < 0)
        {
            return;
        }

        bool oldAvalibleStatus = IsAvalibleToCollector;

        _collectorLevel = (int)collectorLevel;

        if (oldAvalibleStatus != IsAvalibleToCollector)
        {
            BecameAvalible?.Invoke();
        }
    }

    public void Collect()
    {
        if (IsPossibleToCollect)
        {
            _isCollected = true;
            Collected?.Invoke();
        }
    }

    private void Reload()
    {
        _isCollected = false;
        ProcessCollectorLevel(_collectorLevel);
    }

    private string GenerateUniqueId()
    {
        string time = DateTime.Now.Ticks.ToString();
        string random = UnityEngine.Random.Range(1000, 9999).ToString();

        return $"{time}_{random}";
    }
}