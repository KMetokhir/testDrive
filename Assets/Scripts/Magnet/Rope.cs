using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private HingeJoint _joint;

    public void ConnectTarget(Rigidbody target)
    {
        _joint.connectedBody = target;
    }   
}
