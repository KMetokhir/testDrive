using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private List<IAttractable> _activeObjects;

    private CurveMover _mover;

    private void Awake()
    {  
        _activeObjects = new List<IAttractable>();

        _mover = new CurveMover(transform, this);
    }

    private void OnEnable()
    {
        _mover.ReachEnd += OnObjectReachedEnd;
    }

    private void OnDisable()
    {
        _mover.ReachEnd -= OnObjectReachedEnd;
        StopAllCoroutines();
    }

    private void OnObjectReachedEnd(ITransformContainer obj)
    {
        IAttractable attractableObject = obj as IAttractable;

        if (attractableObject != null)
        {
            if (_activeObjects.Contains(attractableObject))
            {
                _activeObjects.Remove(attractableObject);
                attractableObject.TransitToStore();
            }
            else
            {
                throw new System.Exception($"Object {obj} not found in shop");
            }
        }
        else
        {
            throw new System.Exception($"Object {obj}is not IAttractable type");
        }
    }

    private void FixedUpdate()
    {
        if (_activeObjects.Count > 0)
        {
            _mover.Update();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ISeller seller = other.gameObject.GetComponentInParent<ISeller>();

        if (seller != null)
        {
            var newObjects = seller.Buy();

            _activeObjects.AddRange(newObjects);

            List<ITransformContainer> containerList = newObjects.Cast<ITransformContainer>().ToList();
            _mover.MoveObjects(containerList, seller.SellerTransform);
        }
    }
}