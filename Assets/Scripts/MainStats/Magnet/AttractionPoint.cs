using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AttractionPoint
{
    public readonly IAttractable AttractableObject;// get only transform

    private SurfacePoint _surfacePoint;

    public AttractionPoint(SurfacePoint surfacePoint, IAttractable attractable, Transform owner)
    {
        _surfacePoint = surfacePoint ?? throw new ArgumentNullException(nameof(surfacePoint));
        AttractableObject = attractable ?? throw new ArgumentNullException(nameof(attractable));

        AttractableObject.Transform.parent = owner;
    }

    public void Attract(float moveSpeed, float rotationSpeed, float deltatIme)
    {
        if (CheckDistance() == false)
        {
            Move(AttractableObject.Transform, _surfacePoint.WordPosition, moveSpeed, deltatIme);

            Vector3 currentZ = AttractableObject.Transform.forward;
            Vector3 targetZ = _surfacePoint.Normal;

            Quaternion targetRotation = Quaternion.FromToRotation(currentZ, targetZ) * AttractableObject.Transform.rotation;

            AttractableObject.Transform.rotation = Quaternion.Slerp(AttractableObject.Transform.rotation, targetRotation, rotationSpeed * deltatIme);
        }
    }

    public void Clear()
    {
        AttractableObject.Transform.parent = null;
    }

    private void Move(Transform objTransform, Vector3 targetposition, float moveSpeed, float deltatIme)
    {
        objTransform.position = Vector3.MoveTowards(objTransform.position, targetposition, moveSpeed * deltatIme);
    }

    private bool CheckDistance()
    {
        float toleranceDistance = 0.001f;
        return (_surfacePoint.WordPosition - AttractableObject.Transform.position).sqrMagnitude <= toleranceDistance;
    }
}