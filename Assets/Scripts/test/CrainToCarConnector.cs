using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrainToCarConnector : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRb;
    [SerializeField] private CraneSpawner _spawner;

    [SerializeField] private Joint _craneJoint;

    private void OnEnable()
    {
        _spawner.PartSpawned += OnCraneSpawned;
    }

    private void OnCraneSpawned(Crane part)
    { // crane update??
        Crane crane = part;

        if (crane != null) {
                                                                         //ObservablePartSpawner<T> CraneSpawner invoked spawn event AndroidActivityIndicatorStyle after it change
           // _craneJoint = crane.GetComponent<Joint>();
            //_craneJoint.connectedBody = _carRb;
        }
        //throw extension
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
