using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private HingeJoint _joint;

    public void ConnectTarget(Rigidbody target)
    {
        _joint.connectedBody = target;
    }

    public void Disconnect()
    {
        _joint.connectedBody = null;
    }
}
