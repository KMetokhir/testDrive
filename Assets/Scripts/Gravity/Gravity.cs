using UnityEngine;

[RequireComponent(typeof(Rigidbody))]  //add animation curve/ more speed more down force
public class Gravity : MonoBehaviour
{
    [SerializeField] private float _value;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        AddGravity(_value);
    }

    private void AddGravity(float value)
    {
        Vector3 direction = -Vector3.up;
        _rigidbody.AddForce(direction * value);
    }
}