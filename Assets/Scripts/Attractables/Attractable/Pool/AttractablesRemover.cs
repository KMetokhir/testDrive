using UnityEngine;
using Zenject;

public class AttractablesRemover<T> : PoollableRemover<T>
    where T : Attractable
{
    [SerializeField] private AttractableDataHandler<T> _dataHandler;       

    protected override void Subscribe(T obj)
    {
        obj.Stored += OnStored;
    }

    protected override void Unsubscribe(T obj)
    {
        obj.Stored -= OnStored;
    }

    private void OnStored(Attractable attractable)
    {
        if (attractable is not T typed)
        {
            throw new System.Exception($"attractable is not {nameof(T)} type");            
        }

        Debug.Log("Object stored");

        _dataHandler.RemoveById(attractable.Id, _sceneLoadHandler.SceneName);

        ReturnToPool(typed);
    }
}