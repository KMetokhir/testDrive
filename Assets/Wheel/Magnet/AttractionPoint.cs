using System;
using UnityEngine;

public class AttractionPoint
{
    public readonly IAttractable AttractableObject;// move in inner mover??

    private SurfacePoint _surfacePoint;

    public Vector3 Position => _surfacePoint.WordPosition;
    public Vector3 Normal=> _surfacePoint.Normal;

    public AttractionPoint(SurfacePoint surfacePoint, IAttractable attractable)
    {
        _surfacePoint = surfacePoint ?? throw new ArgumentNullException(nameof(surfacePoint));
        AttractableObject = attractable ?? throw new ArgumentNullException(nameof(attractable));

        attractable.PositionChanged += OnPositionChanged;
    }

    public event Action<AttractionPoint> ObjectAttracted;

    private void OnPositionChanged(Vector3 position)
    {
        float toleranceDistance = 0.2f; // to a separate library, same in attractableObj

        if ((_surfacePoint.WordPosition - position).sqrMagnitude <= toleranceDistance)
        {
            ObjectAttracted?.Invoke(this);
        }
    }
}