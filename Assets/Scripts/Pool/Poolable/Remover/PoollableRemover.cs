using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PoollableRemover<T> : MonoBehaviour
    where T : MonoBehaviour, IPoollable
{
    [SerializeField] private ObjectPool<T> _pool;

    [Inject]
    protected readonly SceneLoadHandler _sceneLoadHandler;

    private readonly List<T> _activeObjects = new();

    private void OnEnable()
    {
        _pool.ObjectGeted += OnObjectGetted;
        _sceneLoadHandler.SceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        _pool.ObjectGeted -= OnObjectGetted;
        _sceneLoadHandler.SceneUnloaded -= OnSceneUnloaded;
    }

    private void OnObjectGetted(T obj)
    {
        _activeObjects.Add(obj);

        Debug.Log($"[Remover] Got object {obj.name}");

        Subscribe(obj);
    }

    private void OnSceneUnloaded()
    {
        Debug.Log($"Scene unloaded. Active: {_activeObjects.Count}");

        foreach (var obj in _activeObjects)
        {
            Unsubscribe(obj);
            _pool.PutObject(obj);
        }

        _activeObjects.Clear();
    }

    protected void ReturnToPool(T obj)
    {
        if (!_activeObjects.Remove(obj))
            return;

        Unsubscribe(obj);
        BeforeReturnToPool(obj);

        _pool.PutObject(obj);
    }
    
    protected abstract void Subscribe(T obj);
    protected abstract void Unsubscribe(T obj);
    protected virtual void BeforeReturnToPool(T obj) { }
}