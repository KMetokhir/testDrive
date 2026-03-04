using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveDataProvader : MonoBehaviour, IDriveData
{
    private IDriveData _currentData;
   
    public uint UpgradeLevel => _currentData.UpgradeLevel;

    public uint Acceleration => _currentData.Acceleration;

    public uint MaxSpeed => _currentData.MaxSpeed;

    public float RotationSpeed => _currentData.RotationSpeed;

    public float MaxAngle => _currentData.MaxAngle;

    public float AckermannMultiplier => _currentData.AckermannMultiplier;

    public void Set(IDriveData driveData)
    {
        _currentData = driveData ?? throw new System.Exception($"{nameof(driveData)} is null");
    }
}
