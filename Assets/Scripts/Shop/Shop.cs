using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _curveHeight;
    [SerializeField] private float _moveDelay;

    private List<IAttractable> _objs;

    private CurveMover _mover;

    private void Awake()
    {
        _objs = new List<IAttractable>();

        _mover = new CurveMover(transform, this, _curveHeight, _moveSpeed, _moveDelay);
    }

    private void OnEnable()
    {
        _mover.ReachEnd += OnObjectReachedEnd;
    }

    private void OnDisable()
    {
        _mover.ReachEnd -= OnObjectReachedEnd;
    }

    private void OnObjectReachedEnd(IAttractable obj)
    {

        if (_objs.Contains(obj))
        {
            _objs.Remove(obj);
            obj.TransitToStore();
        }
        else
        {
            Debug.LogWarning($"Object {obj} not found in shop");
        }
    }

    private void FixedUpdate()
    {
        _mover.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        ISeller seller = other.gameObject.GetComponentInParent<ISeller>();

        if (seller != null)
        {
            var newObjects = seller.Buy();

            _objs.AddRange(newObjects);
            _mover.MoveObjects(newObjects);
        }
    }
}