using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity : MonoBehaviour
{  
    [SerializeField] private float _baseForce = 10f;
 
    [SerializeField] private bool _useVelocityCurve = false;
     
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private AnimationCurve _forceBySpeed = AnimationCurve.Linear(0, 1, 10, 2);

    [SerializeField] private float _maxGravityForce = 30f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        float force = CalculateForce();
        AddGravity(force);
    }
   
    private float CalculateForce()
    {
        float force = _baseForce;

        if (_useVelocityCurve)
        {
            float speed = _rigidbody.velocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(speed / _maxSpeed);

            float multiplier = _forceBySpeed.Evaluate(normalizedSpeed);
            force *= multiplier;
        }

        return Mathf.Min(force, _maxGravityForce);
    }

    private void AddGravity(float force)
    {
        Vector3 direction = Vector3.down;
        _rigidbody.AddForce(direction * force, ForceMode.Acceleration);
    }
}