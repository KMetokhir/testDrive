using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// wheell ground checker for all 4 wheels, another forse for x fliip and z fleep, method to calculate angle
public class CarCorrector : MonoBehaviour
{
    public Rigidbody rb;
    public float maxAngle = 30f; // Max angle before correction
    public float correctionForce = 10f; // Force applied to correct flip
    public GroundChecker _wheel;

    void FixedUpdate()
    {
        // Get the car's rotation around the Z axis (assuming upright is Y)
        // float angleZ = NormalizeAngle(transform.eulerAngles.z);
        //  float angleX = NormalizeAngle(transform.eulerAngles.x);

       
        float angleZ = CalculateAngleXY(transform.up, Vector3.up, transform.forward);
        float angleX = CalculateAngleYZ(transform.up, Vector3.up, transform.right);

        Debug.Log(angleZ);

        if (Mathf.Abs(angleZ) > maxAngle && _wheel.IsGrounded() == false)
        {
            // Apply torque to correct the flip
            float torqueDirection = angleZ > 0 ? 1 : -1;
            rb.AddTorque(Vector3.forward * torqueDirection * correctionForce);
            Debug.Log("Use torq");
        }

        if (Mathf.Abs(angleX) > maxAngle && _wheel.IsGrounded() == false)
        {
            Debug.Log("X");
            float torqueDirection = angleX > 0 ? 1 : -1;
            rb.AddTorque(Vector3.right * torqueDirection * correctionForce);
        }

        //  var rotation = Quaternion.FromToRotation(new Vector3(0, 0, transform.forward.z), new Vector3(0, transform.forward.y, transform.forward.z));

        //Debug.Log(CalculateAngle(transform.up, Vector3.up, transform.right));

        //  Debug.Log(rotation.eulerAngles);
        //  Debug.Log("x " + angleX);

        //rigidbody.MoveRotation(Quaternion.FromToRotation(_carTransform.forward, _carTransform.forward + new Vector3(0, -0.01f, 0)) * _carTransform.rotation);

        // rb.AddTorque(Vector3.forward  * correctionForce);
    }

    // Normalize angle to range [-180, 180]
    float NormalizeAngle(float angle)
    {
        while (angle > 180f)
            angle -= 360f;
        while (angle < -180f)
            angle += 360f;
        return angle;
    }

    private float CalculateAngleYZ(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
    {
        float y = 0;
        vectorA = new Vector3(0, vectorA.y, vectorA.z);
        vectorB = new Vector3(0, vectorB.y, vectorB.z);

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }

    private float CalculateAngleXY(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
    {
        float y = 0;
        vectorA = new Vector3(vectorA.x, vectorA.y, 0);
        vectorB = new Vector3(vectorB.x, vectorB.y, 0);

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }
}
