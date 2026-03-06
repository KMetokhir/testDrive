using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DriveDataProvader : CarComponentProvider<IDriveData>, IDriveData
{
    public uint UpgradeLevel => Component.UpgradeLevel;

    public uint Acceleration => Component.Acceleration;

    public uint MaxSpeed => Component.MaxSpeed;

    public float RotationSpeed => Component.RotationSpeed;

    public float MaxAngle => Component.MaxAngle;

    public float AckermannMultiplier => Component.AckermannMultiplier;

    public DriveDataProvader(SignalBus signalBus) : base(signalBus) { }
}
