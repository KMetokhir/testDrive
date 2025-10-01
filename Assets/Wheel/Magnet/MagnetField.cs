using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class MagnetField : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private bool _isMoving;
    // private List<IAttractable> _objectsToMove;
    private List<IAttractable> _attractedObjects;
    private List<AttractionPoint> _attractionPoints;

    public float gap = 1f; // interval between points
    public Vector3 _cubeSize;
    public List<SurfacePoint> _surfacePoints;

    private void Awake()
    {
        _cubeSize = transform.localScale;

        _isMoving = false;
        // _objectsToMove = new List<IAttractable>();
        _attractedObjects = new List<IAttractable>();
        _attractionPoints = new List<AttractionPoint>();
        _surfacePoints = new List<SurfacePoint>();

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
                _cubeSize += new Vector3(0.5f, 0.5f, 0.5f);
                GenerateSurfacePoints(_cubeSize);
            }

            SurfacePoint surfacePoint = GetClosestPoint(obj.Transform.position);

            AttractionPoint point = new AttractionPoint(surfacePoint, obj);
            point.ObjectAttracted += OnObjectAttracted;

            _attractionPoints.Add(point);
            _surfacePoints.Remove(surfacePoint);
        }

        _isMoving = true;
    }

    private void OnObjectAttracted(AttractionPoint point)
    {
        _attractionPoints.Remove(point);

        IAttractable atrractedObject = point.AttractableObject;
        _attractedObjects.Add(point.AttractableObject);
        point.AttractableObject.Transform.parent = transform;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            if (_attractionPoints == null || _attractionPoints.Count == 0)
            {
                Debug.Log("MovingFalse");
                _isMoving = false;
            }

            foreach (AttractionPoint point in _attractionPoints)
            {
                IAttractable attractable = point.AttractableObject;
              //  attractable.Transform.position = Vector3.MoveTowards(attractable.Transform.position, point.Position, _moveSpeed * Time.fixedDeltaTime);


                // Get current and target Z axes as vectors
                Vector3 currentZ = attractable.Transform.forward; // Assuming forward is Z axis
                Vector3 targetZ = point.Normal;

                // Calculate the rotation needed to align Z axes
                Quaternion targetRotation = Quaternion.FromToRotation(currentZ, targetZ) * attractable.Transform.rotation;

                // Smoothly interpolate towards the target rotation
                attractable.Transform.rotation = Quaternion.Slerp(attractable.Transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);



                // attractable.Transform.rotation = Quaternion.AngleAxis(_rotationSpeed * Time.fixedDeltaTime, Vector3.up);

                //attractable.Transform.rotation = Quaternion.Euler(10+Time.fixedDeltaTime, 10, 10);
            }
        }
    }

    /*  private void OnTriggerEnter(Collider other)
      {
          Debug.Log("enter trigger");

          if (other.TryGetComponent(out IAttractable attractable))
          {
              Debug.Log("enter");
              if (TryGetPoint(attractable, _attractionPoints, out AttractionPoint point))
              {
                  _attractionPoints.Remove(point);
                  _attractedObjects.Add(point.AttractableObject);
                  point.AttractableObject.Transform.parent = transform;
              }
          }
      }*/

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

    /* private bool TryGetPoint(IAttractable attractable, List<AttractionPoint> points, out AttractionPoint point)
     {
         point = points.First(point => point.AttractableObject == attractable);

         return point != null;
     }*/

    /* private void OnCollisionEnter(Collision collision)
     {
         Debug.Log("enter1");
         if (collision.collider.TryGetComponent(out IAttractable attractable))
         {
             Debug.Log("enter");
             if (_objectsToMove.Contains(attractable))
             {
                 _objectsToMove.Remove(attractable);
                 _attractedObjects.Add(attractable);
                 attractable.Transform.parent = transform;
             }
         }
     }*/

    void GenerateSurfacePoints(Vector3 cubeSize)
    {
        _surfacePoints.Clear();

        Vector3 testNormal = Vector3.right;

        // Half sizes for positioning
        float halfX = cubeSize.x / 2f;
        float halfY = cubeSize.y / 2f;
        float halfZ = cubeSize.z / 2f;

        // Generate points on each face
        // Front and Back faces (Z)
        for (float x = -halfX; x <= halfX; x += gap)
        {
            for (float y = -halfY; y <= halfY; y += gap)
            {
                SurfacePoint pointFront = new SurfacePoint(new Vector3(x, y, halfZ), testNormal, transform);
                SurfacePoint pointBack = new SurfacePoint(new Vector3(x, y, -halfZ), testNormal, transform);

                _surfacePoints.Add(pointFront);
                _surfacePoints.Add(pointBack);

                /*_surfacePoints.Add(new Vector3(x, y, halfZ)); // Front
                _surfacePoints.Add(new Vector3(x, y, -halfZ)); // Back*/
            }
        }

        // Left and Right faces (X)
        for (float y = -halfY; y <= halfY; y += gap)
        {
            for (float z = -halfZ; z <= halfZ; z += gap)
            {
                SurfacePoint pointRight = new SurfacePoint(new Vector3(halfX, y, z), Vector3.right, transform);
                SurfacePoint pointLeft = new SurfacePoint(new Vector3(-halfX, y, z), Vector3.left, transform);

                _surfacePoints.Add(pointRight);
                _surfacePoints.Add(pointLeft);

                /* _surfacePoints.Add(new Vector3(halfX, y, z)); // Right
                 _surfacePoints.Add(new Vector3(-halfX, y, z)); // Left*/
            }
        }

        // Top and Bottom faces (Y)
        for (float x = -halfX; x <= halfX; x += gap)
        {
            for (float z = -halfZ; z <= halfZ; z += gap)
            {
                SurfacePoint pointTop = new SurfacePoint(new Vector3(x, halfY, z), testNormal, transform);
                SurfacePoint pointBottom = new SurfacePoint(new Vector3(x, -halfY, z), testNormal, transform);

                _surfacePoints.Add(pointTop);
                _surfacePoints.Add(pointBottom);

                /* _surfacePoints.Add(new Vector3(x, halfY, z)); // Top
                 _surfacePoints.Add(new Vector3(x, -halfY, z)); // Bottom*/
            }
        }
    }
}
