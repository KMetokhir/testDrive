using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttractableTest : MonoBehaviour, IAttractable
{
    public bool IsActive { get; private set; }
    public Transform Transform => transform;

    [SerializeField] private float _toleranceDistance;

    private Vector3 _currentPosition;    

    public event Action<Vector3> PositionChanged;

    private void Awake()
    {
        IsActive = true;
        _currentPosition = transform.position;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void FixedUpdate()
    {
        if((_currentPosition - transform.position).sqrMagnitude>= _toleranceDistance)
        {
            PositionChanged?.Invoke(transform.position);
        }
    }
}
