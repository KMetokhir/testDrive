using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering;

public class CarPlaceObserver : MonoBehaviour
{
    private Transform _parent;

    List<GameObject> _objects = new List<GameObject>();

    

    private void Awake()
    {           
        _parent= transform.parent;

        
   
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

}

private void OnCarSpawned(Rigidbody carRigidbody)
{

       /* foreach (var obj in _objects)
        {
            obj.gameObject.transform.parent = carRigidbody.transform.parent;
        }*/

      /*  transform.parent = carRigidbody.transform.parent;
    */
    /* _magnet.transform.parent = null;
     transform.parent = null;
     _rope.ConnectTarget(_magnet.GetComponent<Rigidbody>());*/
}


private void OnCarStartSpawn(Rigidbody carRigidbody)
{
       /* _objects.Clear();
        _objects.Add(GetComponentInChildren<Crane>().gameObject);
        _objects.Add(GetComponentInChildren<Magnet>().gameObject);

        foreach(var obj in _objects)
        {
            obj.gameObject.transform.parent = _parent;
        }*/

     //   transform.parent = _parent;
        /* _magnet.transform.parent = carRigidbody.transform; // Maybe hav to create new joint if needded
         _rope.ConnectTarget(null);
         transform.parent = carRigidbody.transform;*/
    }
}
