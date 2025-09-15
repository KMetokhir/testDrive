using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Rigidbody _carBody;
    [SerializeField] private float _speed;
    [SerializeField] float _gravityModyfire;
    [SerializeField] float _maxAngle;
    [SerializeField] float _angle;
    [SerializeField] float _rotationStep;
    

    private Vector3 _direction;   

    // Start is called before the first frame update
    void Start()
    {
        _angle = 0;
        _direction = _carBody.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            _rigidbody.AddRelativeForce(_direction * _speed);

        }

        if (Input.GetKey("s"))
        {
            _rigidbody.AddRelativeForce(-_direction * _speed);
        }


       
        Debug.Log("sign+ "+ Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(_carBody.transform.forward, transform.TransformDirection(_direction)))));

            if (Input.GetKey("d"))
        {
            Quaternion rotation = transform.rotation;

            Debug.Log(Vector3.Angle(_carBody.transform.forward, transform.TransformDirection(_direction)));

            // rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime);
            //  _direction = (rotation * _direction).normalized; // нужно ли нормализировать



            if (Approximately(CalculateAngleXZPlane(_carBody.transform.forward, transform.TransformDirection(_direction)), _maxAngle,2) == false)
            {
               
                _direction = Quaternion.AngleAxis(_rotationStep, Vector3.up) * _direction;
              //  Debug.Log(CalculateAngleXZPlane(_carBody.transform.forward, _direction));
            }
            
        }

        if (Input.GetKey("a"))
        {
            if (Approximately(CalculateAngleXZPlane(_carBody.transform.forward, transform.TransformDirection(_direction)), -_maxAngle, 2) == false)
            {
                Debug.Log(Vector3.Angle(_carBody.transform.forward, transform.TransformDirection(_direction)));
                _direction = Quaternion.AngleAxis(-_rotationStep, Vector3.up) * _direction;
                Debug.Log(CalculateAngleXZPlane(_carBody.transform.forward, _direction));
            }
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
