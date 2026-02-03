using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public abstract class AttractableRemover<T> : MonoBehaviour // 
    where T : Attractable
{
    [SerializeField] private ObjectPool<T> _pool;
    [SerializeField] private AttractableDataHandler _dataHandler;
    [SerializeField] private SceneLoadHandler _sceneLoadHanddler;

    private List<T> _activeObjects = new List<T>();

    private void OnEnable()
    {
        _pool.ObjectGeted += OnObjectGetted;
        _sceneLoadHanddler.SceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded()
    {
        Debug.Log("Sce umloaded active " + _activeObjects.Count);

        foreach (var obj in _activeObjects)
        {
            obj.Collected -= PutToPool;
            _pool.PutObject(obj as T);
        }

        _activeObjects.Clear();
    }

    private void OnDisable()
    {
        _pool.ObjectGeted -= OnObjectGetted;
        _sceneLoadHanddler.SceneUnloaded -= OnSceneUnloaded;
    }

    private void OnObjectGetted(T attractable)
    {
        Debug.Log("IN Remover getted from poool " + attractable.Id);
        _activeObjects.Add(attractable);
        attractable.Collected += PutToPool;
    }

    private void PutToPool(Attractable attractable)
    {
        attractable.Collected -= PutToPool;
        _activeObjects.Remove(attractable as T);

        Debug.Log("IN Remover object collected");
        _dataHandler.RemoveById(attractable.Id, _sceneLoadHanddler.SceneName);
        _pool.PutObject(attractable as T);
    }
}

/* [SerializeField] private ObjectPool<Attractable> _pool;

 public event Action EnemyRemoved;

 private void Awake()
 {
     BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
     boxCollider.isTrigger = true;
 }

 private void OnEnable()
 {
     _pool.ObjectGeted += OnObjectGetted;
 }

 private void OnDisable()
 {
     _pool.ObjectGeted -= OnObjectGetted;
 }

 private void OnObjectGetted(Attractable attractable
     \)
 {
     enemy.Destroyed += OnEnemyDestroyed;
 }
 private void OnEnemyDestroyed(Enemy enemy)
 {
     enemy.Destroyed -= OnEnemyDestroyed;
     EnemyRemoved?.Invoke();
     _pool.PutObject(enemy);
 }
 private void OnTriggerEnter2D(Collider2D collision)
 {
     if (collision.TryGetComponent(out Enemy enemy))
     {
         enemy.Destroy();
     }
 }*/

