using System;
using UnityEngine;

public class DriveSettings : MonoBehaviour, IDriveData
{

    [SerializeField] private SpeedUpgrader _upgrader;

    public event Action Upgraded;

    public uint UpgradeLevel { get; private set; }

    public uint Acceleration { get; private set; }

    public uint MaxSpeed { get; private set; }

    public float RotationSpeed { get; private set; }

    public float MaxAngle { get; private set; }

    public float AckermannMultiplier { get; private set; }


    private void OnEnable()
    {
        _upgrader.UpgradeExecuted += SetNewStats;
    }

    private void OnDisable()
    {
        _upgrader.UpgradeExecuted -= SetNewStats;
    }

    private void SetNewStats(IDriveData data)
    {
        UpgradeLevel = data.UpgradeLevel;
        Acceleration = data.Acceleration;
        MaxSpeed = data.MaxSpeed;
        RotationSpeed = data.RotationSpeed;
        MaxAngle = data.MaxAngle;
        AckermannMultiplier = data.AckermannMultiplier;

        Upgraded?.Invoke();
    }
}
