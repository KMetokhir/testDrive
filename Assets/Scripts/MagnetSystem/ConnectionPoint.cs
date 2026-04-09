using UnityEngine;

public class ConnectionPoint : MonoBehaviour, IConnectable
{
    public Transform ConnectionTransform => transform;
}