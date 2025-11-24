using System;
using System.Collections.Generic;
using UnityEngine;

public class MagnetField : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private List<IAttractable> _attractedObjects;
    private List<AttractionPoint> _attractionPoints;

    public float _gap = 1f; // interval between points    
    public Vector3 _cubeSize;
    public Transform PhysicCube;
    public List<SurfacePoint> _surfacePoints;

    private void Awake()
    {
        _cubeSize = PhysicCube.localScale;
   
        _attractionPoints = new List<AttractionPoint>();
        _surfacePoints = new List<SurfacePoint>();

        GenerateSurfacePoints(_cubeSize);
    }

    public void AddToField(IAttractable attractable) // TEMP
    {
        List<IAttractable> attractables = new List<IAttractable>();
        attractables.Add(attractable);
        AddToField(attractables);
    }

    public void Clear()
    {
        foreach (AttractionPoint point in _attractionPoints)
        {
            point.Clear();
        }

        _attractionPoints.Clear();
        _surfacePoints.Clear();

        _cubeSize = PhysicCube.localScale;
        GenerateSurfacePoints(_cubeSize);
    }

    public void AddToField(List<IAttractable> attractables)
    {
        if (attractables == null || attractables.Count == 0)
        {
            throw new NullReferenceException(nameof(attractables));
        }

        foreach (IAttractable obj in attractables)
        {
            if (_surfacePoints.Count == 0)
            {
                _cubeSize += new Vector3(0.5f, 0.5f, 0.5f); // add variable gap2
                GenerateSurfacePoints(_cubeSize);
            }

            SurfacePoint surfacePoint = GetClosestPoint(obj.Transform.position);

            AttractionPoint point = new AttractionPoint(surfacePoint, obj, transform);           

            _attractionPoints.Add(point);
            _surfacePoints.Remove(surfacePoint);
        }       
    }

    private void FixedUpdate()
    {
        foreach (AttractionPoint point in _attractionPoints)
        {
            point.Attract(_moveSpeed, _rotationSpeed, Time.fixedDeltaTime);
        }
    }

    private SurfacePoint GetClosestPoint(Vector3 objectPosition)
    {
        if (_surfacePoints == null || _surfacePoints.Count == 0)
        {
            throw new NullReferenceException(nameof(_surfacePoints));
        }

        SurfacePoint closestPoint = null;
        float minDistance = float.MaxValue;
        float distance;

        foreach (SurfacePoint point in _surfacePoints)
        {
            distance = (objectPosition - point.WordPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closestPoint = point;
            }
        }

        return closestPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (_surfacePoints != null)
        {
            foreach (SurfacePoint point in _surfacePoints)
            {
                Gizmos.DrawRay(point.WordPosition, (point.Normal /*transform.TransformDirection(point._localNormal)*/) * 2);
            }
        }
    }

    private void GenerateSurfacePoints(Vector3 cubeSize)
    {
        _surfacePoints.Clear();

        Vector3 testNormal = Vector3.right;

        float halfX = cubeSize.x / 2f;
        float halfY = cubeSize.y / 2f;
        float halfZ = cubeSize.z / 2f;

        float gapFromEdge = _gap / 2;

        for (float x = -halfX + gapFromEdge; x < halfX - gapFromEdge; x += _gap)
        {
            for (float y = -halfY + gapFromEdge; y < halfY - gapFromEdge; y += _gap)
            {
                SurfacePoint pointFront = new SurfacePoint(new Vector3(x, y, halfZ), Vector3.forward, transform);
                SurfacePoint pointBack = new SurfacePoint(new Vector3(x, y, -halfZ), Vector3.back, transform);

                _surfacePoints.Add(pointFront);
                _surfacePoints.Add(pointBack);
            }
        }

        for (float y = -halfY + gapFromEdge; y <= halfY - gapFromEdge; y += _gap)
        {
            for (float z = -halfZ + gapFromEdge; z <= halfZ - gapFromEdge; z += _gap)
            {
                SurfacePoint pointRight = new SurfacePoint(new Vector3(halfX, y, z), Vector3.right, transform);
                SurfacePoint pointLeft = new SurfacePoint(new Vector3(-halfX, y, z), Vector3.left, transform);

                _surfacePoints.Add(pointRight);
                _surfacePoints.Add(pointLeft);
            }
        }

        for (float x = -halfX + gapFromEdge; x <= halfX - gapFromEdge; x += _gap)
        {
            for (float z = -halfZ + gapFromEdge; z <= halfZ - gapFromEdge; z += _gap)
            {
                SurfacePoint pointBottom = new SurfacePoint(new Vector3(x, -halfY, z), Vector3.down, transform);

                _surfacePoints.Add(pointBottom);
            }
        }
    }
}
