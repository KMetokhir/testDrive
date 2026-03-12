using System;
using UnityEngine;
using Zenject;

public class CarBodyProvider : CarComponentProvider<ICarBody>, ICarBody
{
    public Rigidbody Rigidbody => Component.Rigidbody;
    public Vector3 ForwardDirection => Component.ForwardDirection;
    public Vector3 DownDirection => Component.DownDirection;

    public CarBodyProvider(SignalBus signalBus) : base(signalBus) { }
}
