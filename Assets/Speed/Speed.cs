using System;
using UnityEngine;

public class Speed : MonoBehaviour, ISpeed
{
    [SerializeField] private SpeedUpgrader _upgrader;
    [SerializeField] private uint _value;
    [SerializeField] private uint _maxSpeed;

    public uint Value => _value;
    public uint MaxSpeed => _maxSpeed;

    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;
    }

    private void SetNewStats(ISpeedUpgradeData data)
    {
        _value = data.Acceleration;
        _maxSpeed = data.MaxSpeed;
    }
}