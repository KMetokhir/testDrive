using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Suspension : MonoBehaviour
{    
    [SerializeField] private float _springStrength = 50000f;
    [SerializeField] private float _springDamping = 100f;
    [SerializeField] private float _springDistance = 0.3f;

    private ICarBody _carBody;// add interface
    private Rigidbody _wheelRb;
    private ConfigurableJoint _joint;

    private void Awake()
    {
        _wheelRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _carBody = FindAnyObjectByType<Car>();

        _joint = _wheelRb.gameObject.AddComponent<ConfigurableJoint>();
        _joint.connectedBody = _carBody.Rigidbody;

        _joint.xMotion = ConfigurableJointMotion.Locked;
        _joint.yMotion = ConfigurableJointMotion.Limited;
        _joint.zMotion = ConfigurableJointMotion.Locked;

        SoftJointLimitSpring limitSpring = new SoftJointLimitSpring
        {
            spring = _springStrength,
            damper = _springDamping
        };

        _joint.linearLimitSpring = limitSpring;

        SoftJointLimit limit = new SoftJointLimit
        {
            limit = _springDistance
        };
        _joint.linearLimit = limit;

        _joint.angularXMotion = ConfigurableJointMotion.Locked;
        _joint.angularYMotion = ConfigurableJointMotion.Locked;
        _joint.angularZMotion = ConfigurableJointMotion.Locked;
    }
}