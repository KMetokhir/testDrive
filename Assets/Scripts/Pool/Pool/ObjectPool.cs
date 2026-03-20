using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool<T> : MonoBehaviour
    where T : MonoBehaviour, IPoollable
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    private DiContainer _containerDi;

    private Queue<T> _pool;
    private HashSet<T> _activeObjects;

    public event Action<T> ObjectGeted;

    [Inject]
    public void Construct(DiContainer container)
    {
        _containerDi = container;
    }

    private void Awake()
    {
        _pool = new Queue<T>();
        _activeObjects = new HashSet<T>();
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {           
            T newObject = _containerDi
                .InstantiatePrefabForComponent<T>(_prefab, _container);

            newObject.Activate();

            ObjectGeted?.Invoke(newObject);
            _activeObjects.Add(newObject);

            return newObject;
        }

        T objectFromPool = _pool.Dequeue();
        objectFromPool.Activate();

        ObjectGeted?.Invoke(objectFromPool);
        _activeObjects.Add(objectFromPool);

        return objectFromPool;
    }

    public void PutObject(T poolObject)
    {
        _pool.Enqueue(poolObject);
        _activeObjects.Remove(poolObject);      
        poolObject.Deactivate();
        poolObject.Transform.parent = _container.transform;
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