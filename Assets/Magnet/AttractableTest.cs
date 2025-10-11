using System;
using UnityEngine;

public class AttractableTest : MonoBehaviour, IAttractable
{
    [SerializeField] private float _toleranceDistance;
    [SerializeField] private uint _weight;
    [SerializeField] private uint _cost;

    private Vector3 _currentPosition;

    public bool IsActive { get; private set; }
    public Transform Transform => transform;
    public uint Weight => _weight;
    public uint Cost => _cost;

    /*public event Action<Vector3> PositionChanged;*/

    private void OnValidate()
    {
        if (_weight == 0)
        {
            throw new Exception("Weght = 0");
        }

        if (_cost == 0)
        {
            throw new Exception("Cost = 0");
        }
    }

    private void Awake()
    {
        IsActive = true;
        _currentPosition = transform.position;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

   

  /*  private void FixedUpdate()
    {
        if ((_currentPosition - transform.position).sqrMagnitude >= _toleranceDistance)
        {
            PositionChanged?.Invoke(transform.position);
        }
    }*/


}
