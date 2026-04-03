using UnityEngine;

public static class AngleOnPlaneCalculator
{
    public static float CalculateAngle(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
    {
        vectorA = Vector3.ProjectOnPlane(vectorA, normal).normalized;
        vectorB = Vector3.ProjectOnPlane(vectorB, normal).normalized;

        float sign = Mathf.Sign(Vector3.Dot(normal, Vector3.Cross(vectorA, vectorB)));

        return Vector3.Angle(vectorA, vectorB) * sign;
    }
}
