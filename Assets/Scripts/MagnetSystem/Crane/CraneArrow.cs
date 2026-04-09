using UnityEngine;

public class CraneArrow : ConnectableBase
{
    protected override Vector3 GetConnectedAnchor(Transform target, Vector3 position)
    {
        throw new System.NotImplementedException($"{GetType()}  autoConfig joint only");
    }
}