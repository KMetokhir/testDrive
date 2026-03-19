using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMover
{
    private readonly float _height = 20f;
    private readonly float _speed = 1f;
    private readonly float _moveDelay = 0.2f;

    private Transform _endPoint;
    private Transform _startPoint;

    private List<MovingObject> _movingObjects = new List<MovingObject>();

    private MonoBehaviour _coroutineRunner;

    public CurveMover(Transform endpoint, MonoBehaviour coroutineRunner)
    {
        _endPoint = endpoint;
        _coroutineRunner = coroutineRunner;
    }

    public event Action<ITransformContainer> ReachEnd;

    public void MoveObjects(List<ITransformContainer> objects, Transform startPoint)
    {
        _startPoint = startPoint;
        _coroutineRunner.StartCoroutine(CreateActiveObjects(objects));
    }

    public void Update()
    {
        for (int i = _movingObjects.Count - 1; i >= 0; i--)
        {
            var movingObject = _movingObjects[i];

            movingObject.IncreaseTime(_speed * Time.deltaTime);

            movingObject.SetPosition(BezierCurvePoint(movingObject.Time, _startPoint.position, _endPoint.position, _height));

            if (movingObject.Time >= 1f)
            {
                movingObject.SetPosition(_endPoint.position);
                _movingObjects.RemoveAt(i);
                ReachEnd?.Invoke(movingObject.TransformContainer);
            }
        }
    }

    private IEnumerator CreateActiveObjects(List<ITransformContainer> objects)
    {
        float startTime = 0;

        foreach (var obj in objects)
        {
            obj.Transform.parent = null;
            _movingObjects.Add(new MovingObject(obj, startTime));

            yield return new WaitForSeconds(_moveDelay);
        }
    }

    private Vector3 BezierCurvePoint(float t, Vector3 startPoint, Vector3 endpoint, float yOffset)
    {
        t = Mathf.Clamp01(t);
        Vector3 control = (startPoint + endpoint) * 0.5f;
        control.y += yOffset;
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * startPoint + 2f * oneMinusT * t * control + t * t * endpoint;
    }
}