using UnityEngine;
using Zenject;

// wheell ground checker for all 4 wheels, another forse for x fliip and z fleep, method to calculate angle

/*ENABLED!!!!! need state machine to enable corrector in upgrade state*/
public class CarCorrector : MonoBehaviour
{
    [Inject]
    private ICarBody _carBody;

  //  [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _maxAngleXRotation = 10f;
    [SerializeField] private float _correctionForceXRotation = 10f;
    [SerializeField] private float _maxAngleZRotation = 10f;
    [SerializeField] private float _correctionForceZRotation = 10f;
    [SerializeField] private GroundCheckHandler _groundCheck;

    private void Start()
    {
        Debug.LogError($"groundCheck {_groundCheck == null}  carBody {_carBody == null}");

        
    }

    private void FixedUpdate()
    {
        CorrectZFlip();
        CorrectXFlip();     
    }  

    private void CorrectZFlip()
    {
        float angle = CalculateAngleInPlane(transform.up, Vector3.up, transform.forward);

        if (Mathf.Abs(angle) > _maxAngleZRotation && _groundCheck.IsGrounded() == false)
        {
            float torqueDirection = angle > 0 ? 1 : -1;
            _carBody.Rigidbody.AddTorque(_carBody.Transform.forward * torqueDirection * _correctionForceZRotation);
        }
    }

    private void CorrectXFlip()
    {
        float angle = CalculateAngleInPlane(transform.up, Vector3.up, transform.right);

        if (Mathf.Abs(angle) > _maxAngleXRotation && _groundCheck.IsGrounded() == false)
        {
            float torqueDirection = angle > 0 ? 1 : -1;
            _carBody.Rigidbody.AddTorque(_carBody.Transform.right * torqueDirection * _correctionForceXRotation);
        }
    }

    private float CalculateAngleInPlane(Vector3 vectorA, Vector3 vectorB, Vector3 normal) // to library!!
    {
        CalculateInVEctor3(vectorA,  vectorB, normal);

       vectorA = ProjectOntoPlaneNoNormalize(vectorA, normal);
        vectorB = ProjectOntoPlaneNoNormalize(vectorB, normal);       

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        Debug.LogError("My Method " + Vector3.Angle(vectorA, vectorB) * sign);

        return Vector3.Angle(vectorA, vectorB) * sign;
    }

    private void CalculateInVEctor3(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
    {
        vectorA = Vector3.ProjectOnPlane(vectorA, normal).normalized;
        vectorB = Vector3.ProjectOnPlane(vectorB, normal).normalized;

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        Debug.LogError("Use Unity Vector3 " + Vector3.Angle(vectorA, vectorB) * sign);
    }

    private static Vector3 ProjectOntoPlaneNoNormalize(Vector3 vector, Vector3 planeNormal)
    {
        float denom = Vector3.Dot(planeNormal, planeNormal);

        if (denom == 0f)
        {
            return vector;
        }

        return vector - (Vector3.Dot(vector, planeNormal) / denom) * planeNormal;
    }
}