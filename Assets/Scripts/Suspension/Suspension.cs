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

   // [Inject]
    private ICarBody _carBody;

   // private Rigidbody _carBodyRB; // tmp same as iCARbODY

    private Rigidbody _wheelRb;
    private ConfigurableJoint _joint;
    // private Behaviour _jointBehaviour; // tmp

    /* private void Awake()
     {
         _wheelRb = GetComponent<Rigidbody>();
     }*/

    public void Activate(ICarBody carBody, Rigidbody wheelRb)
    {
        if (_carBody == null && _wheelRb == null)
        {
            _carBody = carBody;
            _wheelRb = wheelRb;

           // _carBodyRB = carBody.Rigidbody; 

            CreateJoint();

            MessageBroker.Default
           .Receive<CarStartSpawn>()
           .Subscribe(msg =>
           OnCarStartSpawn(msg.CarRigidbody))
           .AddTo(this);

            MessageBroker.Default
              .Receive<CarEndSpawn>()
              .Subscribe(msg =>
              OnCarSpawned(msg.CarRigidbody))
              .AddTo(this);
        }
        else
        {
            throw new System.Exception($"Suspension on wheel {this.gameObject} already Activated");
        }
    }

    private void OnCarStartSpawn(Rigidbody carRigidbody)
    {
        DeactivateJoint();
    }

    private void OnCarSpawned(Rigidbody carRigidbody)
    {
        ActivateJoint();
    }

   /* private IEnumerator EnableJointSafely()
    {
        

        yield return new WaitForFixedUpdate();

      
    }*/

    private void CreateJoint()
    {
        _joint = _wheelRb.gameObject.AddComponent<ConfigurableJoint>();
        _joint.connectedBody = _carBody.Rigidbody;

        ActivateJoint();
    }

    private void ActivateJoint()
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

    private void DeactivateJoint()
    {

        if (_joint != null)
        {
            _joint.xMotion = ConfigurableJointMotion.Locked;
            _joint.yMotion = ConfigurableJointMotion.Locked;
            _joint.zMotion = ConfigurableJointMotion.Locked;

            SoftJointLimitSpring limitSpring = new SoftJointLimitSpring
            {
                spring = 0,
                damper = 0
            };

            _joint.linearLimitSpring = limitSpring;

            SoftJointLimit limit = new SoftJointLimit
            {
                limit = 0
            };
            _joint.linearLimit = limit;

            _joint.angularXMotion = ConfigurableJointMotion.Locked;
            _joint.angularYMotion = ConfigurableJointMotion.Locked;
            _joint.angularZMotion = ConfigurableJointMotion.Locked;
        }
    }

    /*  private void OnDisable()
      {
          PlayerPrefs.DeleteAll();
      }*/

    /* void Start()
     {
       //  _carBody = FindAnyObjectByType<Car>();//tmp

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
     }*/
}