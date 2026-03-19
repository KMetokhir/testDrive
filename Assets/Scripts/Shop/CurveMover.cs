using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMover
{
    private Transform _end;
    private float _height;
    private float _speed;
    private float _moveDelay;

    private List<MovingObject> _activeObjects = new List<MovingObject>();

    private MonoBehaviour _coroutineRunner;

    public CurveMover(Transform end, MonoBehaviour coroutineRunner, float height = 3f, float speed = 1f, float moveDelay = 0.2f)
    {
        this._end = end;
        this._height = height;
        this._speed = speed;
        this._moveDelay = moveDelay;
        this._coroutineRunner = coroutineRunner;
    }

    public event Action<IAttractable> ReachEnd;

    public void MoveObjects(List<IAttractable> objects)
    {
        _coroutineRunner.StartCoroutine(CreateActiveObjects(objects));
    }

    public void Update()
    {
        for (int i = _activeObjects.Count - 1; i >= 0; i--)
        {
            var mo = _activeObjects[i];

            mo.t += _speed * Time.deltaTime;
            mo.obj.Transform.position = BezierAuto(mo.t, mo.startPos, _end.position, _height);

            if (mo.t >= 1f)
            {
                mo.obj.Transform.position = _end.position;
                _activeObjects.RemoveAt(i);
                ReachEnd?.Invoke(mo.obj);
            }
        }
    }

    private IEnumerator CreateActiveObjects(List<IAttractable> objects)
    {
        foreach (var obj in objects)
        {
            _activeObjects.Add(new MovingObject
            {
                obj = obj,
                startPos = obj.Transform.position,
                t = 0f
            });

            yield return new WaitForSeconds(_moveDelay);
        }
    }

    private Vector3 BezierAuto(float t, Vector3 startPoint, Vector3 endpoint, float yOffset)
    {
        t = Mathf.Clamp01(t);
        Vector3 control = (startPoint + endpoint) * 0.5f;
        control.y += yOffset;
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * startPoint + 2f * oneMinusT * t * control + t * t * endpoint;
    }

    private class MovingObject
    {
        public IAttractable obj;
        public Vector3 startPos;
        public float t;
    }
}