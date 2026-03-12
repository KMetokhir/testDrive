using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Zenject;

public abstract class AttractableRemover<T> : MonoBehaviour // 
    where T : Attractable // used only onCollected and ID => Icollectable instead
{
    [SerializeField] private ObjectPool<T> _pool;
    [SerializeField] private AttractableDataHandler<T> _dataHandler;

    [Inject]
    private SceneLoadHandler _sceneLoadHanddler; // INJECT!

    private List<T> _activeObjects = new List<T>();

    private void OnEnable()
    {
        _pool.ObjectGeted += OnObjectGetted;
        _sceneLoadHanddler.SceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        _pool.ObjectGeted -= OnObjectGetted;
        _sceneLoadHanddler.SceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded()
    {
        Debug.Log("Sce umloaded active " + _activeObjects.Count);

        foreach (var obj in _activeObjects)
        {
            obj.Stored -= PutToPool;
            _pool.PutObject(obj as T);
        }

        _activeObjects.Clear();
    }

    private void OnObjectGetted(T attractable)
    {
        Debug.Log("IN Remover getted from poool " + attractable.Id);
        _activeObjects.Add(attractable);
        attractable.Stored += PutToPool;
    }

    private void PutToPool(Attractable attractable)
    {
        attractable.Stored -= PutToPool;
        _activeObjects.Remove(attractable as T);

        Debug.Log("IN Remover object collected");
        _dataHandler.RemoveById(attractable.Id, _sceneLoadHanddler.SceneName);
        _pool.PutObject(attractable as T);
    }
}
