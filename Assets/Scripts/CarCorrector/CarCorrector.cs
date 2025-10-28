using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// wheell ground checker for all 4 wheels, another forse for x fliip and z fleep, method to calculate angle

ENABLED!!!!!
public class CarCorrector : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float _maxAngleXRotation = 10f;
    [SerializeField] private float _correctionForceXRotation = 10f;
    [SerializeField] private float _maxAngleZRotation = 10f;
    [SerializeField] private float _correctionForceZRotation = 10f;
    [SerializeField] private GroundChecker _wheel;

    void FixedUpdate()
    {
        CorrectZFlip();
        CorrectXFlip();
    }

    private void CorrectZFlip()
    {
        float angle = CalculateAngleInPlane(transform.up, Vector3.up, transform.forward);

        if (Mathf.Abs(angle) > _maxAngleZRotation && _wheel.IsGrounded() == false)
        {
            float torqueDirection = angle > 0 ? 1 : -1;
            rb.AddTorque(rb.transform.forward * torqueDirection * _correctionForceZRotation);
        }
    }

    private void CorrectXFlip()
    {
        float angle = CalculateAngleInPlane(transform.up, Vector3.up, transform.right);

        if (Mathf.Abs(angle) > _maxAngleXRotation && _wheel.IsGrounded() == false)
        {
            float torqueDirection = angle > 0 ? 1 : -1;
            rb.AddTorque(rb.transform.right * torqueDirection * _correctionForceXRotation);
        }
    }

    private float CalculateAngleInPlane(Vector3 vectorA, Vector3 vectorB, Vector3 normal) // to library!!
    {
        vectorA = ProjectOntoPlaneNoNormalize(vectorA, normal);
        vectorB = ProjectOntoPlaneNoNormalize(vectorB, normal);

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }

    public static Vector3 ProjectOntoPlaneNoNormalize(Vector3 v, Vector3 planeNormal)
    {
        float denom = Vector3.Dot(planeNormal, planeNormal);

        if (denom == 0f)
            return v;

        return v - (Vector3.Dot(v, planeNormal) / denom) * planeNormal;
    }
}
