using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SOUser : MonoBehaviour
{
    [SerializeField] TestSO _so;

    [Inject]
    public void Construct(TestSO so)
    {
        _so = so;
    }

    public event Action<int> ChangeValue;


    private void OnEnable()
    {
        _so.SubscribeToMonoBehaviour(this);
    }

    private void OnDisable()
    {
        _so.UnsubscribeFromMonoBehaviour(this);
    }


    private void Start()
    {
        ChangeValue?.Invoke(10);
    }
}
