//using System;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class Suspension : MonoBehaviour
{
    [SerializeField] private float _springStrength = 50000f;
    [SerializeField] private float _springDamping = 100f;
    [SerializeField] private float _springDistance = 0.3f;

    private ICarBody _carBody;

    private Rigidbody _wheelRb;
    private ConfigurableJoint _joint;
 
    public void Activate(ICarBody carBody, Rigidbody wheelRb)
    {
        if (_carBody == null && _wheelRb == null)
        {
            _carBody = carBody;
            _wheelRb = wheelRb;          

            CreateJoint();           
        }
        else
        {
            throw new System.Exception($"Suspension on wheel {this.gameObject} already Activated");
        }
    }
   

    private void CreateJoint()
    {
        _joint = _wheelRb.gameObject.AddComponent<ConfigurableJoint>();
        _joint.connectedBody = _carBody.Rigidbody;

        SetSpringSettings();
    }

    private void SetSpringSettings()
    {
       
        if (_joint != null)
        {

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
}