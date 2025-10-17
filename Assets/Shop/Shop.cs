using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
   
    private List<IAttractable> _objs;

    private void Awake()
    {
        _objs = new List<IAttractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ISeller seller))
        {
            _objs = seller.Buy();
        }
    }

    private void FixedUpdate()
    {
        foreach (var obj in _objs)
        {
            Debug.Log(obj.ToString());
            Move(obj.Transform, transform.position, _moveSpeed, Time.fixedDeltaTime);   
        
        }
    }

    private void Move(Transform objTransform, Vector3 targetposition, float moveSpeed, float deltatIme)
    {
        objTransform.position = Vector3.MoveTowards(objTransform.position, targetposition, moveSpeed * deltatIme);
    }
}
