using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DrivingWheel : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

   

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

   

    public void AddRelativeForce(Vector3 direction, float force)
    {
        _rigidbody.AddRelativeForce(direction * force);
    }

    public void AddForce(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force);
    }
}
