using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Driver : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Rigidbody _carBody;
    [SerializeField] private float _speed;
    [SerializeField] float _gravityModyfire;
    
    
   // [SerializeField] float _maxAngle;
   // [SerializeField] float _angle;
    [SerializeField] float _rotationStep;

   // [SerializeField] private WheelRotator _rotator;

    [SerializeField] private GroundChecker _groundChecker;    

    private Vector3 _direction;   

    // Start is called before the first frame update
    void Start()
    {
       // _angle = 0;
      //  _direction = _carBody.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        /*    if (_groundChecker.IsGrounded(this.transform.position,1.2f)==false)
            {
                Debug.Log("Fly");
                return;
            }*/
       //_direction = _rotator._wheelDirection;

        if (Input.GetKey("w"))
        {
         //  _direction = _rotator._wheelDirection;

           // _rigidbody.AddRelativeForce(_rotator._wheelDirection * _speed);

        }

        if (Input.GetKey("s"))
        {
         //   _direction = _rotator._wheelDirection;
         //   _rigidbody.AddRelativeForce(-_rotator._wheelDirection * _speed);
        }


       
       // Debug.Log(CalculateAngleXZPlane(_carBody.transform.forward, transform.TransformDirection(_direction)));

            if (Input.GetKeyDown("d"))
        {
            /* Quaternion rotation = transform.rotation;

             Debug.Log(Vector3.Angle(_carBody.transform.forward, transform.TransformDirection(_direction)));

             // rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime);
             //  _direction = (rotation * _direction).normalized; // нужно ли нормализировать



             if (Approximately(CalculateAngleXZPlane(_carBody.transform.forward, transform.TransformDirection(_direction)), _maxAngle,2) == false)
             {

                 _direction = Quaternion.AngleAxis(_rotationStep, Vector3.up) * _direction;
               //  Debug.Log(CalculateAngleXZPlane(_carBody.transform.forward, _direction));
             }*/
           /* if (_rotator != null)
            {
               // _direction = _rotator._wheelDirection;
                _rotator.RotateWheel(_direction, 40);
            }*/
            
        }

        if (Input.GetKeyDown("a"))
        {
            /* if (Approximately(CalculateAngleXZPlane(_carBody.transform.forward, transform.TransformDirection(_direction)), -_maxAngle, 2) == false)
             {
                 Debug.Log(Vector3.Angle(_carBody.transform.forward, transform.TransformDirection(_direction)));
                 _direction = Quaternion.AngleAxis(-_rotationStep, Vector3.up) * _direction;
                 Debug.Log(CalculateAngleXZPlane(_carBody.transform.forward, _direction));
             }*/

          /*  if (_rotator != null)
            {
              //  _direction = _rotator._wheelDirection;
                _rotator.RotateWheel(_direction, -40);
            }*/
        }


           
    }

    private bool Approximately(float a, float b, int equalFactor)
    {
        return Mathf.Abs(a - b) <= equalFactor;
    }

    private float CalculateAngleXZPlane(Vector3 vectorA, Vector3 vectorB)
    {
        float y = 0;
        vectorA = new Vector3(vectorA.x, y, vectorA.z);
        vectorB = new Vector3(vectorB.x, y, vectorB.z);

        float sign = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }

    void OnDrawGizmos()
    {   
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Vector3 enPoint = transform.position + _carBody.transform.forward * 20;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(_direction) * 20);
    }
}
