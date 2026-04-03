using UnityEngine;
using Zenject;

public class CarCorrector : MonoBehaviour
{
    [Inject]
    private ICarBody _carBody;

    [SerializeField] private float _maxAngleXRotation = 10f;
    [SerializeField] private float _correctionForceXRotation = 10f;
    [SerializeField] private float _maxAngleZRotation = 10f;
    [SerializeField] private float _correctionForceZRotation = 10f;
    [SerializeField] private GroundCheckHandler _groundCheck;

    private void FixedUpdate()
    {
        CorrectZFlip();
        CorrectXFlip();
    }

    private void CorrectZFlip()
    {
        float angle = AngleOnPlaneCalculator.CalculateAngle(transform.up, Vector3.up, transform.forward);

        if (Mathf.Abs(angle) > _maxAngleZRotation && _groundCheck.IsGrounded() == false)
        {
            float torqueDirection = angle > 0 ? 1 : -1;
            _carBody.Rigidbody.AddTorque(_carBody.Transform.forward * torqueDirection * _correctionForceZRotation);
        }
    }

    private void CorrectXFlip()
    {
        float angle = AngleOnPlaneCalculator.CalculateAngle(transform.up, Vector3.up, transform.right);

        if (Mathf.Abs(angle) > _maxAngleXRotation && _groundCheck.IsGrounded() == false)
        {
            float torqueDirection = angle > 0 ? 1 : -1;
            _carBody.Rigidbody.AddTorque(_carBody.Transform.right * torqueDirection * _correctionForceXRotation);
        }
    }
}