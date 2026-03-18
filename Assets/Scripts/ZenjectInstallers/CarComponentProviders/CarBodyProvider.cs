using UnityEngine;
using Zenject;

public class CarBodyProvider : CarComponentProvider<ICarBody>, ICarBody
{
    public Rigidbody Rigidbody => Component?.Rigidbody;
    public Vector3 ForwardDirection => Component?.ForwardDirection ?? Vector3.zero;
    public Vector3 DownDirection => Component?.DownDirection ?? Vector3.zero;

    public CarBodyProvider(SignalBus signalBus) : base(signalBus) { }
}