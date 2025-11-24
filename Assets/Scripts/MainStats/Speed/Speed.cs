using System;
using UnityEngine;

public class Speed : MonoBehaviour, ISpeed, IUpgradable
{
    [SerializeField] private SpeedUpgrader _upgrader;
    [SerializeField] private uint _value;
    [SerializeField] private uint _maxSpeed;

    public event Action Upgraded;

    public uint Value => _value;
    public uint MaxSpeed => _maxSpeed;

    public uint UpgradeLevel { get; private set; }

    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
    }

    private void SetNewStats(ISpeedUpgradeData data)
    {
        _value = data.Acceleration;
        _maxSpeed = data.MaxSpeed;

        UpgradeLevel = data.UpgradeLevel;

        Upgraded?.Invoke();
    }
}