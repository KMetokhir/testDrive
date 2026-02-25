using OpenCover.Framework.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour
    where T : MonoBehaviour, IPoollable // iPoolable with methodes Activate deactivate
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    private Queue<T> _pool;
    private HashSet<T> _activeObjects;

    public event Action<T> ObjectGeted;

    private void Awake()
    {
        _pool = new Queue<T>();
        _activeObjects = new HashSet<T>();
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            T newObject = Instantiate(_prefab);
            newObject.Transform.parent = _container;
            ObjectGeted?.Invoke(newObject);

            _activeObjects.Add(newObject);

            return newObject;
        }

        T objectFromPool = _pool.Dequeue();
        // objectFromPool.gameObject.SetActive(true);

        objectFromPool.Activate();
       ObjectGeted?.Invoke(objectFromPool);

        _activeObjects.Add(objectFromPool);

        return objectFromPool;
    }

    public void PutObject(T poolObject)
    {
        _pool.Enqueue(poolObject);
        _activeObjects.Remove(poolObject);
        // poolObject.gameObject.SetActive(false);
        poolObject.Deactivate();
    }

    public void Restart()
    {
        HashSet<T> tmpActiveObjects = new HashSet<T>(_activeObjects);

        foreach (T activeObjects in tmpActiveObjects)
        {
            PutObject(activeObjects);
        }
    }
}
