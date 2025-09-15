using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Spring : MonoBehaviour
{
    [SerializeField] public Rigidbody carBody;
    [SerializeField] public float springStrength = 50000f;
    [SerializeField] public float springDamping = 100f;
    [SerializeField] public float springDistance = 0.3f;

    private Rigidbody _wheelRb;

    private ConfigurableJoint _joint;

    private void Awake()
    {
        _wheelRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // Add ConfigurableJoint to the wheel
        _joint = _wheelRb.gameObject.AddComponent<ConfigurableJoint>();
        _joint.connectedBody = carBody;

        // Set joint to act like a spring
        _joint.xMotion = ConfigurableJointMotion.Locked;
        _joint.yMotion = ConfigurableJointMotion.Limited;
        _joint.zMotion = ConfigurableJointMotion.Locked;

        // Configure linear limit to simulate spring
        SoftJointLimitSpring limitSpring = new SoftJointLimitSpring
        {
            spring = springStrength,
            damper = springDamping

        };

        _joint.linearLimitSpring = limitSpring;

        // Set the linear limit distance
        SoftJointLimit limit = new SoftJointLimit
        {
            limit = springDistance
        };
        _joint.linearLimit = limit;

        // Optional: Lock angular motion
        _joint.angularXMotion = ConfigurableJointMotion.Locked;
        _joint.angularYMotion = ConfigurableJointMotion.Locked;
        _joint.angularZMotion = ConfigurableJointMotion.Locked;

    }
}