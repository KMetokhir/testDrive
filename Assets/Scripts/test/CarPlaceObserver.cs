using System;
using UniRx;
using UnityEngine;
using Zenject;
using static CarDestroyer;


public class CarPlaceObserver : MonoBehaviour
{

    private void Awake()
    {

        MessageBroker.Default
              .Receive<CarStartSpawn>()
               .Subscribe(msg =>
               OnCarStartSpawn(msg.CarRigidbody))
               .AddTo(this);

        MessageBroker.Default
          .Receive<CarEndSpawn>()
              .Subscribe(msg =>
              OnCarSpawned(msg.CarRigidbody))
              .AddTo(this);

        MessageBroker.Default
         .Receive<CarDestroied>()
             .Subscribe(_ =>
             OnCarDestroied())
             .AddTo(this);
    }

    private void OnCarDestroied()
    {
        Destroy(gameObject);
    }

    private void OnCarSpawned(Rigidbody carRigidbody)
    {
        transform.parent = transform.parent.root;
    }

    private void OnCarStartSpawn(Rigidbody carRigidbody)
    {
        transform.parent = carRigidbody.transform;
    }
}
